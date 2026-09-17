using Microsoft.Data.SqlClient;
using TWLumber.Client.Models;

namespace TWLumber.Client.Data;

/// <summary>
/// Maps <see cref="SqlDataReader"/> rows onto <see cref="SaleItem"/> instances using the
/// underlying SALEITEMS column names. Mapping is tolerant of columns that are absent from the
/// result set (e.g. a partial <c>SELECT</c>) and of <c>NULL</c> values.
/// </summary>
internal static class SaleItemMapper
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
    /// Maps the reader's current row to a <see cref="SaleItem"/>.
    /// </summary>
    public static SaleItem Map(SqlDataReader reader, IReadOnlyDictionary<string, int> columns)
    {
        return new SaleItem
        {
            SalesOrder = GetString(reader, columns, "SALESORDER"),
            SaleItemNumber = GetString(reader, columns, "SALEITEM"),
            PrSaleItem = GetString(reader, columns, "PRSALEITEM"),
            Type = GetDecimal(reader, columns, "TYPE"),
            Description = GetString(reader, columns, "DESCRIPTION"),
            Quantity = GetDecimal(reader, columns, "QUANTITY"),
            QuantityUnit = GetString(reader, columns, "QUANTITYUNIT"),
            PriceUnit = GetString(reader, columns, "PRICEUNIT"),
            DisplayUnit = GetString(reader, columns, "DISPLAYUNIT"),
            Discount = GetDecimal(reader, columns, "DISCOUNT"),
            PriceBasis = GetDecimal(reader, columns, "PRICEBASIS"),
            TruckCo = GetString(reader, columns, "TRUCKCO"),
            ReadyDate = GetDateTime(reader, columns, "READYDATE"),
            ShipStartDate = GetDateTime(reader, columns, "SHIPSTARTDATE"),
            ShipEndDate = GetDateTime(reader, columns, "SHIPENDDATE"),
            ShipTo = GetString(reader, columns, "SHIPTO"),
            CareOf = GetString(reader, columns, "CAREOF"),
            Destination = GetString(reader, columns, "DESTINATION"),
            Dock = GetString(reader, columns, "DOCK"),
            Vessel = GetString(reader, columns, "VESSEL"),
            Voyage = GetString(reader, columns, "VOYAGE"),
            Booking = GetString(reader, columns, "BOOKING"),
            Route = GetString(reader, columns, "ROUTE"),
            ProdNotes = GetString(reader, columns, "PRODNOTES"),
            MoriNet = GetString(reader, columns, "MORINET"),
            AllowThicker = GetBool(reader, columns, "ALLOWTHICKER"),
            AllowWider = GetBool(reader, columns, "ALLOWWIDER"),
            Wane = GetString(reader, columns, "WANE"),
            Wrap = GetString(reader, columns, "WRAP"),
            AntiStain = GetString(reader, columns, "ANTISTAIN"),
            EndSeal = GetString(reader, columns, "ENDSEAL"),
            Packaging = GetString(reader, columns, "PACKAGING"),
            PackageSize = GetString(reader, columns, "PACKAGESIZE"),
            LabelName = GetString(reader, columns, "LABELNAME"),
            LabelCount = GetDecimal(reader, columns, "LABELCOUNT"),
            LabelPrinter = GetString(reader, columns, "LABELPRINTER"),
            Logo = GetString(reader, columns, "LOGO"),
            RunLink = GetString(reader, columns, "RUNLINK"),
            RunItemLink = GetString(reader, columns, "RUNITEMLINK"),
            AllowIncrement = GetBool(reader, columns, "ALLOWINCREMENT"),
            Location = GetString(reader, columns, "LOCATION"),
            GradeRule = GetString(reader, columns, "GRADERULE"),
            Species = GetString(reader, columns, "SPECIES"),
            GrossAdjust = GetDecimal(reader, columns, "GROSSADJUST"),
            NetAdjust = GetDecimal(reader, columns, "NETADJUST"),
            Priority = GetDecimal(reader, columns, "PRIORITY"),
            Lot = GetString(reader, columns, "LOT"),
            Contract = GetString(reader, columns, "CONTRACT"),
            AllowLonger = GetBool(reader, columns, "ALLOWLONGER"),
            QuantityI = GetDecimal(reader, columns, "QUANTITYI"),
            QuantityM = GetDecimal(reader, columns, "QUANTITYM"),
            QuantityK = GetDecimal(reader, columns, "QUANTITYK"),
            QuantityP = GetDecimal(reader, columns, "QUANTITYP"),
            QuantityL = GetDecimal(reader, columns, "QUANTITYL"),
            QuantityA = GetDecimal(reader, columns, "QUANTITYA"),
            QuantityB = GetDecimal(reader, columns, "QUANTITYB"),
            QuantityT = GetDecimal(reader, columns, "QUANTITYT"),
            GeneratedDesc = GetString(reader, columns, "GENERATEDDESC"),
            ColourSpecification = GetString(reader, columns, "COLOURSPECIFICATION"),
            WidthSpecification = GetString(reader, columns, "WIDTHSPECIFICATION"),
            LengthSpecification = GetString(reader, columns, "LENGTHSPECIFICATION"),
            MoistureContent = GetString(reader, columns, "MOISTURECONTENT"),
            TransportType = GetString(reader, columns, "TRANSPORTTYPE"),
            PaymentMethod = GetString(reader, columns, "PAYMENTMETHOD"),
            HasLogo = GetBool(reader, columns, "HASLOGO"),
            HasDetail = GetDecimal(reader, columns, "HASDETAIL"),
            OffProductSpec = GetBool(reader, columns, "OFFPRODUCTSPEC"),
            SourceRunItem = GetString(reader, columns, "SOURCERUNITEM"),
            YardNote = GetString(reader, columns, "YardNote"),
            RoutingId = GetNullableInt(reader, columns, "ROUTINGID"),
            ProdNoteId = GetDecimal(reader, columns, "prodnoteID"),
            YardNoteId = GetDecimal(reader, columns, "yardnoteID"),
            IsFlexItem = GetBool(reader, columns, "IsFlexItem"),
            Tarped = GetBool(reader, columns, "Tarped"),
            RecNo = GetDecimal(reader, columns, "RECNO"),
            QuantityN = GetDecimal(reader, columns, "QUANTITYN"),
            SailingDate = GetDateTime(reader, columns, "SAILINGDATE"),
            ArrivalDate = GetDateTime(reader, columns, "ARRIVALDATE"),
            PricingDate = GetDateTime(reader, columns, "PRICINGDATE"),
            PriceBreakdownSetting = GetByte(reader, columns, "PriceBreakdownSetting"),
            OriginalSalesOrder = GetString(reader, columns, "ORIGINALSALESORDER"),
            Pts = GetBool(reader, columns, "PTS"),
            FormulaCode = GetString(reader, columns, "FORMULACODE"),
            PriceTable = GetString(reader, columns, "PRICETABLE"),
            Ready = GetBool(reader, columns, "READY"),
            ProductLevel = GetBool(reader, columns, "PRODUCTLEVEL"),
            ShipToCustCode = GetString(reader, columns, "SHIPTOCUSTCODE"),
            CareOfCustCode = GetString(reader, columns, "CAREOFCUSTCODE"),
            PortOfLoading = GetString(reader, columns, "PORTOFLOADING"),
            PortOfLanding = GetString(reader, columns, "PORTOFLANDING"),
            InsuredAmount = GetDecimal(reader, columns, "INSUREDAMOUNT"),
            PulledItemReady = GetBool(reader, columns, "PULLEDITEMREADY"),
            HeatTreatment = GetString(reader, columns, "HEATTREATMENT"),
            MaterialFrom = GetDecimal(reader, columns, "MATERIALFROM"),
            UserDefinedSort = GetInt(reader, columns, "UserDefinedSort"),
            RecordSource = GetInt(reader, columns, "RECORDSOURCE"),
            SendToProductionPlan = GetBool(reader, columns, "SENDTOPRODUCTIONPLAN"),
            PulledDate = GetDateTime(reader, columns, "PULLEDDATE"),
            FscCertified = GetBool(reader, columns, "FSCCERTIFIED"),
            UserDefinedType = GetString(reader, columns, "UserDefinedType"),
            UserDefinedDescription = GetString(reader, columns, "UserDefinedDescription"),
            CustomUnit = GetString(reader, columns, "CUSTOMUNIT"),
            Weight = GetDecimal(reader, columns, "WEIGHT"),
            CutPart = GetString(reader, columns, "CutPart"),
            LengthGroups = GetString(reader, columns, "LENGTHGROUPS"),
            PaintColor = GetString(reader, columns, "PAINTCOLOR"),
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

    private static byte GetByte(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToByte(v) : (byte)0;

    private static int GetInt(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToInt32(v) : 0;

    private static int? GetNullableInt(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToInt32(v) : null;

    private static decimal GetDecimal(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToDecimal(v) : 0m;

    private static DateTime GetDateTime(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToDateTime(v) : default;
}
