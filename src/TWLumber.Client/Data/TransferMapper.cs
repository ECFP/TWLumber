using Microsoft.Data.SqlClient;
using TWLumber.Client.Models;

namespace TWLumber.Client.Data;

/// <summary>
/// Maps <see cref="SqlDataReader"/> rows onto <see cref="Transfer"/> instances using the underlying
/// TRANSFERS column names. Mapping is tolerant of columns that are absent from the result set
/// (e.g. a partial <c>SELECT</c>) and of <c>NULL</c> values.
/// </summary>
internal static class TransferMapper
{
    /// <summary>
    /// Builds a case-insensitive lookup of column name to ordinal for the current result set.
    /// Build this once per query and reuse it for every row.
    /// </summary>
    public static Dictionary<string, int> BuildColumnMap(SqlDataReader reader)
    {
        var map = new Dictionary<string, int>(reader.FieldCount, StringComparer.OrdinalIgnoreCase);
        for (var i = 0; i < reader.FieldCount; i++)
        {
            map[reader.GetName(i)] = i;
        }

        return map;
    }

    /// <summary>
    /// Maps the reader's current row to a <see cref="Transfer"/>.
    /// </summary>
    public static Transfer Map(SqlDataReader reader, IReadOnlyDictionary<string, int> columns)
    {
        return new Transfer
        {
            TransferId = GetDecimal(reader, columns, "TRANSFERID"),
            TransDate = GetDateTime(reader, columns, "TRANSDATE"),
            FromLocation = GetString(reader, columns, "FROMLOCATION"),
            ToLocation = GetString(reader, columns, "TOLOCATION"),
            Alley = GetString(reader, columns, "ALLEY"),
            Operator = GetString(reader, columns, "OPERATOR"),
            TruckCo = GetString(reader, columns, "TRUCKCO"),
            TruckNum = GetString(reader, columns, "TRUCKNUM"),
            Driver = GetString(reader, columns, "DRIVER"),
            Weight = GetDecimal(reader, columns, "WEIGHT"),
            Notes = GetString(reader, columns, "NOTES"),
            ExportState = GetDecimal(reader, columns, "EXPORTSTATE"),
            PermitNum = GetString(reader, columns, "PERMITNUM"),
            BorderDate = GetDateTime(reader, columns, "BORDERDATE"),
            ContainerNumber = GetString(reader, columns, "CONTAINERNUMBER"),
            SealNumber = GetString(reader, columns, "SEALNUMBER"),
            RecNo = GetDecimal(reader, columns, "RECNO"),
            Run = GetString(reader, columns, "RUN"),
            SalesOrder = GetString(reader, columns, "SALESORDER"),
            CustOrdNum = GetString(reader, columns, "CUSTORDNUM"),
            MillCode = GetDecimal(reader, columns, "MILLCODE"),
            Booking = GetString(reader, columns, "BOOKING"),
            BillTo = GetString(reader, columns, "BILLTO"),
            BillToRecNo = GetDecimal(reader, columns, "BILLTORECNO"),
        };
    }

    private static bool TryGetValue(
        SqlDataReader reader,
        IReadOnlyDictionary<string, int> columns,
        string column,
        out object value)
    {
        if (columns.TryGetValue(column, out var ordinal) && !reader.IsDBNull(ordinal))
        {
            value = reader.GetValue(ordinal);
            return true;
        }

        value = DBNull.Value;
        return false;
    }

    private static string GetString(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToString(v) ?? string.Empty : string.Empty;

    private static decimal GetDecimal(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToDecimal(v) : 0m;

    private static DateTime GetDateTime(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToDateTime(v) : default;
}
