using System.Data;
using Microsoft.Data.SqlClient;
using TWLumber.Client.Exceptions;
using TWLumber.Client.Models;

namespace TWLumber.Client.Data;

/// <summary>
/// Reads <see cref="SaleItem"/> records from the Tallyworks database.
/// Obtain an instance from <see cref="TWClient.SaleItems"/>.
/// </summary>
public sealed class SaleItemsRepository
{
    private readonly TWClient _client;

    internal SaleItemsRepository(TWClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets all line items belonging to a sales order, each with its <see cref="SaleItem.SaleItemDetails"/>.
    /// </summary>
    /// <param name="salesOrder">The parent sales order number, e.g. <c>"672367"</c>.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task<IReadOnlyList<SaleItem>> GetBySalesOrderAsync(string salesOrder, CancellationToken cancellationToken = default)
    {
        var items = await QueryListAsync(
            "SELECT * FROM SaleItems WHERE UPPER(SalesOrder) = UPPER(@SalesOrder)",
            new Dictionary<string, object?> { ["@SalesOrder"] = Normalize(salesOrder) },
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return await AttachDetailsAsync(items, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets all line items belonging to any of the supplied sales orders (each with its details),
    /// in as few queries as the batch limit allows. Used to attach items to a set of sales without
    /// issuing one query per sale.
    /// </summary>
    internal async Task<IReadOnlyList<SaleItem>> GetBySalesOrdersAsync(
        IReadOnlyCollection<string> salesOrders,
        CancellationToken cancellationToken = default)
    {
        var distinct = salesOrders
            .Select(Normalize)
            .Where(value => value.Length > 0)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (distinct.Count == 0)
        {
            return Array.Empty<SaleItem>();
        }

        const int batchSize = SaleItemsBatchLimitException.DefaultLimit;

        IReadOnlyList<SaleItem> items;
        if (distinct.Count <= batchSize)
        {
            // A single batch fits under SQL Server's parameter ceiling — the common case.
            items = await QueryChunkAsync(distinct, cancellationToken).ConfigureAwait(false);
        }
        else if (!_client.AllowLargeIncludeQueries)
        {
            // More sales orders than one batch allows. By default this is treated as a mistake and
            // reported; opting in via TWClientOptions.AllowLargeIncludeQueries splits it into batches.
            throw new SaleItemsBatchLimitException(distinct.Count, batchSize);
        }
        else
        {
            var results = new List<SaleItem>();
            for (var start = 0; start < distinct.Count; start += batchSize)
            {
                var chunk = distinct.GetRange(start, Math.Min(batchSize, distinct.Count - start));
                results.AddRange(await QueryChunkAsync(chunk, cancellationToken).ConfigureAwait(false));
            }

            items = results;
        }

        return await AttachDetailsAsync(items, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Runs a single <c>WHERE SalesOrder IN (...)</c> lookup for one chunk of sales orders.
    /// The chunk must not exceed <see cref="SaleItemsBatchLimitException.DefaultLimit"/> entries.
    /// </summary>
    private async Task<IReadOnlyList<SaleItem>> QueryChunkAsync(
        IReadOnlyList<string> salesOrders,
        CancellationToken cancellationToken)
    {
        var parameters = new Dictionary<string, object?>(salesOrders.Count);
        var placeholders = new string[salesOrders.Count];
        for (var i = 0; i < salesOrders.Count; i++)
        {
            var name = "@so" + i;
            placeholders[i] = "UPPER(" + name + ")";
            parameters[name] = salesOrders[i];
        }

        var sql = $"SELECT * FROM SaleItems WHERE UPPER(SalesOrder) IN ({string.Join(", ", placeholders)})";
        return await QueryListAsync(sql, parameters, cancellationToken: cancellationToken).ConfigureAwait(false);
    }

    private async Task<IReadOnlyList<SaleItem>> QueryListAsync(
        string sql,
        IReadOnlyDictionary<string, object?>? parameters = null,
        CommandType commandType = CommandType.Text,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sql);

        await using var connection = await _client.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var command = CreateCommand(connection, sql, parameters, commandType);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        var results = new List<SaleItem>();
        if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            var columns = SaleItemMapper.BuildColumnMap(reader);
            do
            {
                results.Add(SaleItemMapper.Map(reader, columns));
            }
            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false));
        }

        return results;
    }

    private static string Normalize(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.Trim();
    }

    /// <summary>
    /// Loads the detail rows for the supplied items (batched by sales order) and attaches each set
    /// to its parent item via <see cref="SaleItem.SaleItemDetails"/>.
    /// </summary>
    private async Task<IReadOnlyList<SaleItem>> AttachDetailsAsync(
        IReadOnlyList<SaleItem> items,
        CancellationToken cancellationToken)
    {
        if (items.Count == 0)
        {
            return items;
        }

        var salesOrders = items
            .Select(item => item.SalesOrder)
            .Where(salesOrder => !string.IsNullOrWhiteSpace(salesOrder))
            .ToList();

        var details = await _client.SaleItemDetails
            .GetBySalesOrdersAsync(salesOrders, cancellationToken)
            .ConfigureAwait(false);

        if (details.Count == 0)
        {
            return items;
        }

        var detailsByItem = new Dictionary<string, List<SaleItemDetail>>(StringComparer.Ordinal);
        foreach (var detail in details)
        {
            var key = ItemKey(detail.SalesOrder, detail.SaleItemNumber);
            if (!detailsByItem.TryGetValue(key, out var list))
            {
                list = new List<SaleItemDetail>();
                detailsByItem[key] = list;
            }

            list.Add(detail);
        }

        foreach (var item in items)
        {
            if (detailsByItem.TryGetValue(ItemKey(item.SalesOrder, item.SaleItemNumber), out var list))
            {
                item.SaleItemDetails = list;
            }
        }

        return items;
    }

    /// <summary>
    /// Builds a case-insensitive composite key identifying a sale item by its sales order and item
    /// number, used to match detail rows to their parent item. The separator is a control character
    /// that cannot appear in either value, so distinct pairs never collide.
    /// </summary>
    private static string ItemKey(string salesOrder, string saleItemNumber)
        => salesOrder.Trim().ToUpperInvariant() + (char)31 + saleItemNumber.Trim().ToUpperInvariant();

    private SqlCommand CreateCommand(
        SqlConnection connection,
        string sql,
        IReadOnlyDictionary<string, object?>? parameters,
        CommandType commandType)
    {
        var command = connection.CreateCommand();
        command.CommandText = sql;
        command.CommandType = commandType;
        command.CommandTimeout = _client.CommandTimeoutSeconds;

        if (parameters is not null)
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
            }
        }

        return command;
    }
}
