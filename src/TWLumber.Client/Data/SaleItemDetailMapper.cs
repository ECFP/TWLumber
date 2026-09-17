using Microsoft.Data.SqlClient;
using TWLumber.Client.Models;

namespace TWLumber.Client.Data;

/// <summary>
/// Maps <see cref="SqlDataReader"/> rows onto <see cref="SaleItemDetail"/> instances using the
/// underlying SALEITEMDETAIL column names. Mapping is tolerant of columns that are absent from the
/// result set (e.g. a partial <c>SELECT</c>) and of <c>NULL</c> values.
/// </summary>
internal static class SaleItemDetailMapper
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
    /// Maps the reader's current row to a <see cref="SaleItemDetail"/>.
    /// </summary>
    public static SaleItemDetail Map(SqlDataReader reader, IReadOnlyDictionary<string, int> columns)
    {
        return new SaleItemDetail
        {
            SalesOrder = GetString(reader, columns, "SALESORDER"),
            OriginalSalesOrder = GetString(reader, columns, "ORIGINALSALESORDER"),
            SaleItemNumber = GetString(reader, columns, "SALEITEM"),
            Product = GetString(reader, columns, "PRODUCT"),
            Species = GetString(reader, columns, "SPECIES"),
            ThickNet = GetString(reader, columns, "THICKNET"),
            WidthNet = GetString(reader, columns, "WIDTHNET"),
            LengthNet = GetDecimal(reader, columns, "LENGTHNET"),
            GradeCode = GetString(reader, columns, "GRADECODE"),
            Grain = GetString(reader, columns, "GRAIN"),
            PieceChar1 = GetString(reader, columns, "PIECECHAR1"),
            PieceChar2 = GetString(reader, columns, "PIECECHAR2"),
            State = GetString(reader, columns, "STATE"),
            Planing = GetString(reader, columns, "PLANING"),
            PkgChar1 = GetString(reader, columns, "PKGCHAR1"),
            PkgChar2 = GetString(reader, columns, "PKGCHAR2"),
            Quantity = GetDecimal(reader, columns, "QUANTITY"),
            QuantityI = GetDecimal(reader, columns, "QUANTITYI"),
            QuantityM = GetDecimal(reader, columns, "QUANTITYM"),
            QuantityK = GetDecimal(reader, columns, "QUANTITYK"),
            QuantityP = GetDecimal(reader, columns, "QUANTITYP"),
            QuantityL = GetDecimal(reader, columns, "QUANTITYL"),
            QuantityA = GetDecimal(reader, columns, "QUANTITYA"),
            QuantityB = GetDecimal(reader, columns, "QUANTITYB"),
            QuantityN = GetDecimal(reader, columns, "QUANTITYN"),
            QuantityT = GetDecimal(reader, columns, "QUANTITYT"),
            Price = GetDecimal(reader, columns, "PRICE"),
            GrossPriceAmount = GetDecimal(reader, columns, "GROSSPRICEAMOUNT"),
            NetMillValue = GetDecimal(reader, columns, "NETMILLVALUE"),
            ChargesIncl = GetDecimal(reader, columns, "CHARGES_INCL"),
            ProductPrice = GetDecimal(reader, columns, "PRODUCTPRICE"),
            AllocatedQuantity = GetDecimal(reader, columns, "ALLOCATEDQUANTITY"),
            NoteId = GetDecimal(reader, columns, "NoteID"),
            OriginalGrossPriceAmount = GetDecimal(reader, columns, "ORIGINALGROSSPRICEAMOUNT"),
            RpcCost = GetDecimal(reader, columns, "RPCCOST"),
            AutoGenRecord = GetBool(reader, columns, "AUTOGENRECORD"),
            RecNo = GetDecimal(reader, columns, "RECNO"),
            SendToProductionPlan = GetBool(reader, columns, "sendtoproductionplan"),
            JagPackagesOrdered = GetBool(reader, columns, "JagPackagesOrdered"),
            CustomQuantity = GetDecimal(reader, columns, "CUSTOMQUANTITY"),
            Weight = GetDecimal(reader, columns, "WEIGHT"),
            ExternCode = GetString(reader, columns, "EXTERNCODE"),
            Pts = GetBool(reader, columns, "PTS"),
            FormulaCode = GetString(reader, columns, "FORMULACODE"),
            MarketPrice = GetDecimal(reader, columns, "MARKETPRICE"),
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

    private static bool GetBool(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) && Convert.ToBoolean(v);

    private static decimal GetDecimal(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToDecimal(v) : 0m;
}
