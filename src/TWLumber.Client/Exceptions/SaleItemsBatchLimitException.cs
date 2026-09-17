namespace TWLumber.Client.Exceptions;

/// <summary>
/// Thrown when a request to include <see cref="Models.SaleItem"/> data would need to look up
/// more distinct sales orders in a single batch than SQL Server allows parameters for.
/// When this is thrown, narrow the originating query or load sale items in smaller groups.
/// </summary>
public sealed class SaleItemsBatchLimitException : Exception
{
    /// <summary>
    /// The maximum number of distinct sales orders that can be batched into a single
    /// sale-items lookup. Kept below SQL Server's ~2100 parameter ceiling.
    /// </summary>
    public const int DefaultLimit = 2000;

    /// <summary>
    /// Creates the exception.
    /// </summary>
    /// <param name="requestedCount">The number of distinct sales orders the caller asked to include items for.</param>
    /// <param name="limit">The maximum number allowed in a single batch.</param>
    public SaleItemsBatchLimitException(int requestedCount, int limit)
        : base($"Cannot include sale items for {requestedCount} sales orders in a single batch; " +
               $"the maximum is {limit}. Narrow the query (for example, filter to a smaller set) " +
               $"or load sale items separately per sales order via TWClient.SaleItems.")
    {
        RequestedCount = requestedCount;
        Limit = limit;
    }

    /// <summary>The number of distinct sales orders the caller asked to include items for.</summary>
    public int RequestedCount { get; }

    /// <summary>The maximum number of distinct sales orders allowed in a single batch.</summary>
    public int Limit { get; }
}
