namespace TWLumber.Client;

/// <summary>
/// Configuration options for a <see cref="TWClient"/> instance.
/// </summary>
public sealed class TWClientOptions
{
    /// <summary>
    /// Controls what happens when a single "include sale items" call needs to look up more than
    /// <see cref="SaleItemsBatchLimitException.DefaultLimit"/> distinct sales orders.
    /// <para>
    /// When <c>false</c> (the default) the call throws <see cref="SaleItemsBatchLimitException"/>
    /// before running, so an accidentally broad query is surfaced early rather than silently
    /// loading a very large dataset.
    /// </para>
    /// <para>
    /// When <c>true</c> the client transparently splits the lookup into several batched queries
    /// and combines the results, so arbitrarily large includes succeed.
    /// </para>
    /// <para>
    /// Leave this off unless you understand the consequences:
    /// <list type="bullet">
    ///   <item><description>
    ///   Every matching sale item is held in memory at once. A broad parent query (for example,
    ///   "all sales entered before a date") can pull hundreds of thousands of item rows and
    ///   exhaust memory.
    ///   </description></item>
    ///   <item><description>
    ///   The lookup runs as multiple sequential round-trips, so latency grows with the result size.
    ///   </description></item>
    ///   <item><description>
    ///   It is easy to enable and then trigger unintentionally with an overly broad filter. The
    ///   default limit exists precisely to catch that mistake. If you turn this on, make sure the
    ///   queries you pass <c>includeItems: true</c> are already scoped to a sensible number of sales.
    ///   </description></item>
    /// </list>
    /// </para>
    /// </summary>
    public bool AllowLargeIncludeQueries { get; set; }

    /// <summary>
    /// The connection string used to reach the Tallyworks MSSQL database.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// The default timeout, in seconds, applied to commands executed by the client.
    /// Defaults to 30 seconds.
    /// </summary>
    public int CommandTimeoutSeconds { get; set; } = 30;

    /// <summary>
    /// Optional application name reported to SQL Server for connection tracking.
    /// When set, it is applied to the connection string if one is not already present.
    /// </summary>
    public string? ApplicationName { get; set; } = "TWLumber.Client";

    /// <summary>
    /// Throws if the options are not usable.
    /// </summary>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            throw new ArgumentException(
                "A connection string must be provided.",
                nameof(ConnectionString));
        }

        if (CommandTimeoutSeconds < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(CommandTimeoutSeconds),
                CommandTimeoutSeconds,
                "Command timeout cannot be negative.");
        }
    }
}
