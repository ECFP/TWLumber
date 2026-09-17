using System.Data;
using Microsoft.Data.SqlClient;
using TWLumber.Client.Exceptions;
using TWLumber.Client.Models;

namespace TWLumber.Client.Data;

/// <summary>
/// Reads <see cref="SaleItemDetail"/> records from the Tallyworks database. Details are nested
/// under their parent <see cref="SaleItem"/>, so this repository is internal: callers obtain
/// detail data by loading sale items (which carry their details).
/// </summary>
internal sealed class SaleItemDetailsRepository
{
    private readonly TWClient _client;

    internal SaleItemDetailsRepository(TWClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets every detail row belonging to any of the supplied sales orders, in as few queries as
    /// the batch limit allows. Callers group the results by sales order and sale item.
    /// </summary>
    internal async Task<IReadOnlyList<SaleItemDetail>> GetBySalesOrdersAsync(
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
            return Array.Empty<SaleItemDetail>();
        }

        const int batchSize = SaleItemsBatchLimitException.DefaultLimit;

        if (distinct.Count <= batchSize)
        {
            return await QueryChunkAsync(distinct, cancellationToken).ConfigureAwait(false);
        }

        if (!_client.AllowLargeIncludeQueries)
        {
            throw new SaleItemsBatchLimitException(distinct.Count, batchSize);
        }

        var results = new List<SaleItemDetail>();
        for (var start = 0; start < distinct.Count; start += batchSize)
        {
            var chunk = distinct.GetRange(start, Math.Min(batchSize, distinct.Count - start));
            results.AddRange(await QueryChunkAsync(chunk, cancellationToken).ConfigureAwait(false));
        }

        return results;
    }

    /// <summary>
    /// Runs a single <c>WHERE SalesOrder IN (...)</c> lookup for one chunk of sales orders.
    /// The chunk must not exceed <see cref="SaleItemsBatchLimitException.DefaultLimit"/> entries.
    /// </summary>
    private async Task<IReadOnlyList<SaleItemDetail>> QueryChunkAsync(
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

        var sql = $"SELECT * FROM SaleItemDetail WHERE UPPER(SalesOrder) IN ({string.Join(", ", placeholders)})";
        return await QueryListAsync(sql, parameters, cancellationToken).ConfigureAwait(false);
    }

    private async Task<IReadOnlyList<SaleItemDetail>> QueryListAsync(
        string sql,
        IReadOnlyDictionary<string, object?> parameters,
        CancellationToken cancellationToken)
    {
        await using var connection = await _client.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.CommandType = CommandType.Text;
        command.CommandTimeout = _client.CommandTimeoutSeconds;

        foreach (var parameter in parameters)
        {
            command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
        }

        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        var results = new List<SaleItemDetail>();
        if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            var columns = SaleItemDetailMapper.BuildColumnMap(reader);
            do
            {
                results.Add(SaleItemDetailMapper.Map(reader, columns));
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
}
