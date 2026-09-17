using System.Data;
using Microsoft.Data.SqlClient;
using TWLumber.Client.Models;

namespace TWLumber.Client.Data;

/// <summary>
/// Reads <see cref="Transfer"/> records from the Tallyworks database.
/// Obtain an instance from <see cref="TWClient.Transfers"/>.
/// </summary>
public sealed class TransfersRepository
{
    private readonly TWClient _client;

    internal TransfersRepository(TWClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Gets a single transfer by its transfer id.
    /// </summary>
    /// <param name="transferId">The transfer id, e.g. <c>978949</c>.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The matching <see cref="Transfer"/>, or <c>null</c> if none exists.</returns>
    public Task<Transfer?> GetByTransferIdAsync(decimal transferId, CancellationToken cancellationToken = default)
        => QuerySingleAsync(
            "SELECT * FROM Transfers WHERE TransferID = @TransferId",
            new Dictionary<string, object?> { ["@TransferId"] = transferId },
            cancellationToken);

    /// <summary>
    /// Gets all transfers for a run. A run may have more than one transfer.
    /// </summary>
    /// <param name="run">The run identifier, e.g. <c>"MAC-VALLEY 682117"</c>.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public Task<IReadOnlyList<Transfer>> GetByRunAsync(string run, CancellationToken cancellationToken = default)
        => QueryListAsync(
            "SELECT * FROM Transfers WHERE UPPER(Run) = UPPER(@Run)",
            new Dictionary<string, object?> { ["@Run"] = Normalize(run) },
            cancellationToken);

    private async Task<Transfer?> QuerySingleAsync(
        string sql,
        IReadOnlyDictionary<string, object?> parameters,
        CancellationToken cancellationToken)
    {
        await using var connection = await _client.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var command = CreateCommand(connection, sql, parameters);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        if (!await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            return null;
        }

        var columns = TransferMapper.BuildColumnMap(reader);
        return TransferMapper.Map(reader, columns);
    }

    private async Task<IReadOnlyList<Transfer>> QueryListAsync(
        string sql,
        IReadOnlyDictionary<string, object?> parameters,
        CancellationToken cancellationToken)
    {
        await using var connection = await _client.OpenConnectionAsync(cancellationToken).ConfigureAwait(false);
        await using var command = CreateCommand(connection, sql, parameters);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);

        var results = new List<Transfer>();
        if (await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            var columns = TransferMapper.BuildColumnMap(reader);
            do
            {
                results.Add(TransferMapper.Map(reader, columns));
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

    private SqlCommand CreateCommand(
        SqlConnection connection,
        string sql,
        IReadOnlyDictionary<string, object?> parameters)
    {
        var command = connection.CreateCommand();
        command.CommandText = sql;
        command.CommandType = CommandType.Text;
        command.CommandTimeout = _client.CommandTimeoutSeconds;

        foreach (var parameter in parameters)
        {
            command.Parameters.AddWithValue(parameter.Key, parameter.Value ?? DBNull.Value);
        }

        return command;
    }
}
