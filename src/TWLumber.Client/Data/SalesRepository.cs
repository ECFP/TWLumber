using System.Data;
using Microsoft.Data.SqlClient;
using TWLumber.Client.Enums;
using TWLumber.Client.Models;

namespace TWLumber.Client.Data;

/// <summary>
/// Reads <see cref="Sale"/> records from the Tallyworks database.
/// Obtain an instance from <see cref="TWClient.Sales"/>.
/// </summary>
public sealed class SalesRepository
{
    private readonly TWClient _client;

    internal SalesRepository(TWClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets a single sale by its sales order number.
    /// </summary>
    /// <param name="salesOrder">The sales order number, e.g. <c>"672367"</c>.</param>
    /// <param name="includeItems">When <c>true</c>, the sale's <see cref="Sale.SaleItems"/> are loaded and attached.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The matching <see cref="Sale"/>, or <c>null</c> if none exists.</returns>
    public async Task<Sale?> GetBySalesOrderAsync(
        string salesOrder,
        bool includeItems = false,
        CancellationToken cancellationToken = default)
    {
        var sale = await QuerySingleAsync(
            "SELECT * FROM Sales WHERE UPPER(SalesOrder) = UPPER(@SalesOrder)",
            new Dictionary<string, object?> { ["@SalesOrder"] = Normalize(salesOrder) },
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return await AttachItemsAsync(sale, includeItems, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets all sales for a customer.
    /// </summary>
    /// <param name="custCode">The customer code, e.g. <c>"ECFP"</c>.</param>
    /// <param name="includeItems">When <c>true</c>, each sale's <see cref="Sale.SaleItems"/> are loaded and attached.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task<IReadOnlyList<Sale>> GetByCustomerAsync(
        string custCode,
        bool includeItems = false,
        CancellationToken cancellationToken = default)
    {
        var sales = await QueryListAsync(
            "SELECT * FROM Sales WHERE UPPER(CustCode) = UPPER(@CustCode)",
            new Dictionary<string, object?> { ["@CustCode"] = Normalize(custCode) },
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return await AttachItemsAsync(sales, includeItems, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets all sales with the given status.
    /// </summary>
    /// <param name="status">The sale status, e.g. <see cref="SalesStatus.Closed"/>.</param>
    /// <param name="includeItems">When <c>true</c>, each sale's <see cref="Sale.SaleItems"/> are loaded and attached.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task<IReadOnlyList<Sale>> GetByStatusAsync(
        SalesStatus status,
        bool includeItems = false,
        CancellationToken cancellationToken = default)
    {
        var sales = await QueryListAsync(
            "SELECT * FROM Sales WHERE Status = @Status",
            new Dictionary<string, object?> { ["@Status"] = (int)status },
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return await AttachItemsAsync(sales, includeItems, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets all sales assigned to a salesperson.
    /// </summary>
    /// <param name="salesPerson">The salesperson code, e.g. <c>"MS"</c>.</param>
    /// <param name="includeItems">When <c>true</c>, each sale's <see cref="Sale.SaleItems"/> are loaded and attached.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task<IReadOnlyList<Sale>> GetBySalesPersonAsync(
        string salesPerson,
        bool includeItems = false,
        CancellationToken cancellationToken = default)
    {
        var sales = await QueryListAsync(
            "SELECT * FROM Sales WHERE UPPER(SalesPerson) = UPPER(@SalesPerson)",
            new Dictionary<string, object?> { ["@SalesPerson"] = Normalize(salesPerson) },
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return await AttachItemsAsync(sales, includeItems, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets all sales at a location.
    /// </summary>
    /// <param name="location">The location code, e.g. <c>"ECFP"</c>.</param>
    /// <param name="includeItems">When <c>true</c>, each sale's <see cref="Sale.SaleItems"/> are loaded and attached.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task<IReadOnlyList<Sale>> GetByLocationAsync(
        string location,
        bool includeItems = false,
        CancellationToken cancellationToken = default)
    {
        var sales = await QueryListAsync(
            "SELECT * FROM Sales WHERE UPPER(Location) = UPPER(@Location)",
            new Dictionary<string, object?> { ["@Location"] = Normalize(location) },
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return await AttachItemsAsync(sales, includeItems, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets all sales entered before the given date (exclusive).
    /// </summary>
    /// <param name="date">The upper bound; sales with <c>EntryDate</c> earlier than this are returned.</param>
    /// <param name="includeItems">When <c>true</c>, each sale's <see cref="Sale.SaleItems"/> are loaded and attached.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task<IReadOnlyList<Sale>> GetEnteredBeforeAsync(
        DateTime date,
        bool includeItems = false,
        CancellationToken cancellationToken = default)
    {
        var sales = await QueryListAsync(
            "SELECT * FROM Sales WHERE EntryDate < @Date",
            new Dictionary<string, object?> { ["@Date"] = date },
            cancellationToken: cancellationToken).ConfigureAwait(false);

        return await AttachItemsAsync(sales, includeItems, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Executes a query and returns the first <see cref="Sale"/>, or <c>null</c> when the
    /// query yields no rows.
    /// </summary>
    /// <param name="sql">The SQL query (or stored procedure name — see <paramref name="commandType"/>).</param>
    /// <param name="parameters">
    /// Optional named parameters, e.g. <c>new Dictionary&lt;string, object?&gt; { ["@SalesOrder"] = "SO123" }</c>.
    /// A <c>null</c> value is sent as <c>DBNULL</c>.
    /// </param>
    /// <param name="commandType">How <paramref name="sql"/> is interpreted. Defaults to <see cref="CommandType.Text"/>.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    internal async Task<Sale?> QuerySingleAsync(
        string sql,
        IReadOnlyDictionary<string, object?>? parameters = null,
        CommandType commandType = CommandType.Text,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sql);

        await using var connection = await _client.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var command = CreateCommand(connection, sql, parameters, commandType);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        if (!await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            return null;
        }

        var columns = SaleMapper.BuildColumnMap(reader);
        return SaleMapper.Map(reader, columns);
    }

    /// <summary>
    /// Executes a query and returns every matching <see cref="Sale"/>.
    /// Returns an empty list when there are no rows.
    /// </summary>
    /// <param name="sql">The SQL query (or stored procedure name — see <paramref name="commandType"/>).</param>
    /// <param name="parameters">
    /// Optional named parameters, e.g. <c>new Dictionary&lt;string, object?&gt; { ["@CustCode"] = "ACME" }</c>.
    /// A <c>null</c> value is sent as <c>DBNULL</c>.
    /// </param>
    /// <param name="commandType">How <paramref name="sql"/> is interpreted. Defaults to <see cref="CommandType.Text"/>.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    internal async Task<IReadOnlyList<Sale>> QueryListAsync(
        string sql,
        IReadOnlyDictionary<string, object?>? parameters = null,
        CommandType commandType = CommandType.Text,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sql);

        await using var connection = await _client.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var command = CreateCommand(connection, sql, parameters, commandType);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        var results = new List<Sale>();
        if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            var columns = SaleMapper.BuildColumnMap(reader);
            do
            {
                results.Add(SaleMapper.Map(reader, columns));
            }
            while (await reader.ReadAsync(cancellationToken).ConfigureAwait(false));
        }

        return results;
    }

    /// <summary>
    /// Trims a string lookup value. Case-insensitivity is applied in SQL via <c>UPPER()</c>,
    /// so callers may pass any casing.
    /// </summary>
    private static string Normalize(string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        return value.Trim();
    }

    /// <summary>
    /// Loads and attaches <see cref="Sale.SaleItems"/> for a single sale when requested.
    /// </summary>
    private async Task<Sale?> AttachItemsAsync(Sale? sale, bool includeItems, CancellationToken cancellationToken)
    {
        if (sale is null || !includeItems)
        {
            return sale;
        }

        sale.SaleItems = await _client.SaleItems
            .GetBySalesOrderAsync(sale.SalesOrder, cancellationToken)
            .ConfigureAwait(false);

        return sale;
    }

    /// <summary>
    /// Loads and attaches <see cref="Sale.SaleItems"/> for a set of sales when requested,
    /// using a single batched query rather than one query per sale.
    /// </summary>
    private async Task<IReadOnlyList<Sale>> AttachItemsAsync(
        IReadOnlyList<Sale> sales,
        bool includeItems,
        CancellationToken cancellationToken)
    {
        if (!includeItems || sales.Count == 0)
        {
            return sales;
        }

        var salesOrders = sales
            .Select(sale => sale.SalesOrder)
            .Where(salesOrder => !string.IsNullOrWhiteSpace(salesOrder))
            .ToList();

        var items = await _client.SaleItems
            .GetBySalesOrdersAsync(salesOrders, cancellationToken)
            .ConfigureAwait(false);

        var itemsByOrder = items
            .GroupBy(item => item.SalesOrder, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                group => group.Key,
                group => (IReadOnlyList<SaleItem>)group.ToList(),
                StringComparer.OrdinalIgnoreCase);

        foreach (var sale in sales)
        {
            if (itemsByOrder.TryGetValue(sale.SalesOrder, out var saleItems))
            {
                sale.SaleItems = saleItems;
            }
        }

        return sales;
    }

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
