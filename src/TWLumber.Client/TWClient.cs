using Microsoft.Data.SqlClient;
using TWLumber.Client;
using TWLumber.Client.Data;
using TWLumber.Client.Models;

namespace TWLumber.Client;

/// <summary>
/// Entry point for accessing data stored in a Tallyworks MSSQL database.
/// Instantiate with a <see cref="TWClientOptions"/> (or a connection string)
/// and use the client to issue commands and retrieve model objects.
/// </summary>
public sealed class TWClient
{
    private readonly TWClientOptions _options;
    private readonly string _connectionString;
    private SalesRepository? _sales;
    private SaleItemsRepository? _saleItems;
    private SaleItemDetailsRepository? _saleItemDetails;
    private TransfersRepository? _transfers;

    /// <summary>
    /// Creates a new <see cref="TWClient"/> from the supplied options.
    /// </summary>
    /// <param name="options">Configuration for the client, including the connection string.</param>
    public TWClient(TWClientOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        options.Validate();

        _options = options;
        _connectionString = BuildConnectionString(options);
    }

    /// <summary>
    /// Creates a new <see cref="TWClient"/> using a connection string and default options.
    /// </summary>
    /// <param name="connectionString">The connection string to the Tallyworks database.</param>
    public TWClient(string connectionString)
        : this(new TWClientOptions { ConnectionString = connectionString })
    {
    }

    /// <summary>
    /// The command timeout, in seconds, applied to commands issued by this client.
    /// </summary>
    public int CommandTimeoutSeconds => _options.CommandTimeoutSeconds;

    /// <summary>
    /// Whether large "include sale items" lookups are split into batches instead of throwing.
    /// See <see cref="TWClientOptions.AllowLargeIncludeQueries"/>.
    /// </summary>
    internal bool AllowLargeIncludeQueries => _options.AllowLargeIncludeQueries;

    /// <summary>
    /// Access to <see cref="Sale"/> data in the Tallyworks database.
    /// </summary>
    public SalesRepository Sales => _sales ??= new SalesRepository(this);

    /// <summary>
    /// Access to <see cref="SaleItem"/> data in the Tallyworks database.
    /// </summary>
    public SaleItemsRepository SaleItems => _saleItems ??= new SaleItemsRepository(this);

    /// <summary>
    /// Internal access to <see cref="SaleItemDetail"/> data. Details are exposed to callers only
    /// as <see cref="SaleItem.SaleItemDetails"/>, so this is not part of the public surface.
    /// </summary>
    internal SaleItemDetailsRepository SaleItemDetails => _saleItemDetails ??= new SaleItemDetailsRepository(this);

    /// <summary>
    /// Access to <see cref="Transfer"/> data in the Tallyworks database.
    /// </summary>
    public TransfersRepository Transfers => _transfers ??= new TransfersRepository(this);

    /// <summary>
    /// Opens and verifies a connection to the database.
    /// </summary>
    /// <returns><c>true</c> if a connection could be opened; otherwise throws.</returns>
    public async Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        return connection.State == System.Data.ConnectionState.Open;
    }

    /// <summary>
    /// Creates a new <see cref="SqlConnection"/> for the configured database.
    /// The caller is responsible for opening and disposing it.
    /// </summary>
    internal SqlConnection CreateConnection() => new(_connectionString);

    /// <summary>
    /// Creates and opens a new <see cref="SqlConnection"/> for the configured database.
    /// The caller is responsible for disposing it.
    /// </summary>
    internal async Task<SqlConnection> OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        var connection = CreateConnection();
        try
        {
            await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
            return connection;
        }
        catch
        {
            await connection.DisposeAsync().ConfigureAwait(false);
            throw;
        }
    }

    private static string BuildConnectionString(TWClientOptions options)
    {
        var builder = new SqlConnectionStringBuilder(options.ConnectionString);

        if (!string.IsNullOrWhiteSpace(options.ApplicationName)
            && string.IsNullOrWhiteSpace(builder.ApplicationName))
        {
            builder.ApplicationName = options.ApplicationName;
        }

        return builder.ConnectionString;
    }
}
