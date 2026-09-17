using System.Data;
using Microsoft.Data.SqlClient;
using TWLumber.Client.Enums;
using TWLumber.Client.Models;

namespace TWLumber.Client.Data;

/// <summary>
/// Maps <see cref="SqlDataReader"/> rows onto <see cref="Sale"/> instances using the
/// underlying SALES column names. Mapping is tolerant of columns that are absent from the
/// result set (e.g. a partial <c>SELECT</c>) and of <c>NULL</c> values.
/// </summary>
internal static class SaleMapper
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
    /// Maps the reader's current row to a <see cref="Sale"/>.
    /// </summary>
    public static Sale Map(SqlDataReader reader, IReadOnlyDictionary<string, int> columns)
    {
        return new Sale
        {
            RecNo = GetDecimal(reader, columns, "RECNO"),
            SalesOrder = GetString(reader, columns, "SALESORDER"),
            OriginalSalesOrder = GetString(reader, columns, "ORIGINALSALESORDER"),
            CustCode = GetString(reader, columns, "CUSTCODE"),
            PosOrder = GetBool(reader, columns, "POSORDER"),
            PosCustName = GetString(reader, columns, "POSCUSTNAME"),
            CustOrdNum = GetString(reader, columns, "CUSTORDNUM"),
            LocalCust = GetString(reader, columns, "LOCALCUST"),
            EntryDate = GetDateTime(reader, columns, "ENTRYDATE"),
            Currency = GetString(reader, columns, "CURRENCY"),
            Priority = GetDecimal(reader, columns, "PRIORITY"),
            SalesPerson = GetString(reader, columns, "SALESPERSON"),
            Market = GetString(reader, columns, "MARKET"),
            Fob = GetString(reader, columns, "FOB"),
            TransportType = GetString(reader, columns, "TRANSPORTTYPE"),
            SaleNotes = GetString(reader, columns, "SALENOTES"),
            MillNotes = GetString(reader, columns, "MILLNOTES"),
            CustNotes = GetString(reader, columns, "CUSTNOTES"),
            Terms = GetString(reader, columns, "TERMS"),
            Contact = GetString(reader, columns, "CONTACT"),
            Status = (SalesStatus)GetInt(reader, columns, "STATUS"),
            Revised = GetDateTime(reader, columns, "REVISED"),
            NoExport = GetBool(reader, columns, "NOEXPORT"),
            Logo = GetString(reader, columns, "LOGO"),
            Class = GetString(reader, columns, "CLASS"),
            CertificationNo = GetString(reader, columns, "CertificationNo"),
            ItemType = GetDecimal(reader, columns, "ITEMTYPE"),
            Location = GetString(reader, columns, "LOCATION"),
            QuotaType = GetString(reader, columns, "QUOTATYPE"),
            Mark = GetString(reader, columns, "MARK"),
            OverrideTaxA = GetBool(reader, columns, "OVERRIDETAXA"),
            OverrideTaxB = GetBool(reader, columns, "OVERRIDETAXB"),
            OverrideTaxC = GetBool(reader, columns, "OVERRIDETAXC"),
            OverrideTaxD = GetBool(reader, columns, "OVERRIDETAXD"),
            OverrideTaxAPc = GetDecimal(reader, columns, "OVERRIDETAXAPC"),
            OverrideTaxBPc = GetDecimal(reader, columns, "OVERRIDETAXBPC"),
            OverrideTaxCPc = GetDecimal(reader, columns, "OVERRIDETAXCPC"),
            OverrideTaxDPc = GetDecimal(reader, columns, "OVERRIDETAXDPC"),
            TransportSetupRequired = GetDecimal(reader, columns, "TRANSPORTSETUPREQUIRED"),
            CityId = GetDecimal(reader, columns, "CityID"),
            InitDateTime = GetDateTime(reader, columns, "InitDateTime"),
            Unassigned = GetBool(reader, columns, "UNASSIGNED"),
            HideQo = GetBool(reader, columns, "HIDEQO"),
            Description = GetString(reader, columns, "DESCRIPTION"),
            IsAlteringCustOrdNum = GetDecimal(reader, columns, "IsAlteringCustOrdnum"),
            PrintTagDetail = GetBool(reader, columns, "PrintTagDetail"),
            OriginMill = GetString(reader, columns, "ORIGINMILL"),
            DefaultQu = GetString(reader, columns, "DEFAULTQU"),
            DefaultPu = GetString(reader, columns, "DEFAULTPU"),
            DefaultPb = GetDecimal(reader, columns, "DEFAULTPB"),
            ParentSalesOrder = GetString(reader, columns, "PARENTSALESORDER"),
            MasterOrder = GetBool(reader, columns, "MASTERORDER"),
            CpRunType = GetDecimal(reader, columns, "CPRUNTYPE"),
            CertificationNote = GetString(reader, columns, "CertificationNote"),
            CertificationPct = GetDecimal(reader, columns, "CertificationPct"),
            CertificationUnit = GetString(reader, columns, "CertificationUnit"),
            PrimaryApproval = GetBool(reader, columns, "PRIMARYAPPROVAL"),
            SecondaryApproval = GetBool(reader, columns, "SECONDARYAPPROVAL"),
            PrimaryApprover = GetString(reader, columns, "PRIMARYAPPROVER"),
            SecondaryApprover = GetString(reader, columns, "SECONDARYAPPROVER"),
            Edited = GetBool(reader, columns, "EDITED"),
            SalesOrderRevision = GetInt(reader, columns, "SALESORDERREVISION"),
            IsCertified = GetBool(reader, columns, "IsCertified"),
            ConfirmationPrinted = GetBool(reader, columns, "CONFIRMATIONPRINTED"),
            ReceivedByShipping = GetBool(reader, columns, "RECEIVEDBYSHIPPING"),
            BelowThresholdSale = GetBool(reader, columns, "BelowThresholdSale"),
            TruckNum = GetString(reader, columns, "TRUCKNUM"),
            UserDefined1 = GetString(reader, columns, "USERDEFINED1"),
            PermitNum = GetString(reader, columns, "PERMITNUM"),
            Ready = GetBool(reader, columns, "Ready"),
            ShipToTable = GetByte(reader, columns, "ShipToTable"),
            ShipToRecNo = GetDecimal(reader, columns, "ShipToRecno"),
            Prepayment = GetDecimal(reader, columns, "PREPAYMENT"),
            CareOfTable = GetByte(reader, columns, "CareOfTable"),
            CareOfRecNo = GetDecimal(reader, columns, "CareOfRecno"),
            IsVmiOrder = GetBool(reader, columns, "ISVMIORDER"),
            FscCertified = GetBool(reader, columns, "FSCCERTIFIED"),
            TarpRequired = GetBool(reader, columns, "TARPREQUIRED"),
            Freight = GetDecimal(reader, columns, "FREIGHT"),
            AutoTallyCompleted = GetBool(reader, columns, "AUTOTALLYCOMPLETED"),
            Mode = GetString(reader, columns, "MODE"),
            LumberTag = GetString(reader, columns, "LUMBERTAG"),
            SalesOrderNeedsCesoReview = GetBool(reader, columns, "SALESORDERNEEDSCESOREVIEW"),
            SendToRemanRunOnly = GetBool(reader, columns, "SENDTOREMANRUNONLY"),
            LinkedItemSetToHoldFromCustomer = GetBool(reader, columns, "LinkedItemSetToHoldFromCustomer"),
            IsCustomSalesOrder = GetBool(reader, columns, "ISCUSTOMSALESORDER"),
            CreatedBy = GetString(reader, columns, "CREATEDBY"),
            LastEditedBy = GetString(reader, columns, "LASTEDITEDBY"),
            MillCode = GetDecimal(reader, columns, "MILLCODE"),
            HotShipmentCompleted = GetBool(reader, columns, "HOTSHIPMENTCOMPLETED"),
            VmiLocation = GetString(reader, columns, "VMILocation"),
            EndUserCustCode = GetString(reader, columns, "ENDUSERCUSTCODE"),
            CommissionPercentage = GetDecimal(reader, columns, "CommissionPercentage"),
            ShipmentStatus = GetString(reader, columns, "SHIPMENTSTATUS"),
            Agent = GetString(reader, columns, "Agent"),
            SoldByCustCode = GetString(reader, columns, "SOLDBYCUSTCODE"),
            CustGradeMeasure = GetBool(reader, columns, "CustGradeMeasure"),
            CommissionPayBy = GetInt(reader, columns, "CommissionPayBy"),
            CommissionDollarsPer = GetDecimal(reader, columns, "CommissionDollarsPer"),
            CommissionUnit = GetString(reader, columns, "CommissionUnit"),
            Phone = GetString(reader, columns, "PHONE"),
            Email = GetString(reader, columns, "EMAIL"),
            SelectNewCustomer = GetByte(reader, columns, "SELECTNEWCUSTOMER"),
            ContainerNumber = GetString(reader, columns, "CONTAINERNUMBER"),
            PortOfEntry = GetString(reader, columns, "PORTOFENTRY"),
            PortOfDest = GetString(reader, columns, "PORTOFDEST"),
            Equipment = GetString(reader, columns, "EQUIPMENT"),
            FilterToFollow = GetBool(reader, columns, "FILTERTOFOLLOW"),
            EndUser = GetBool(reader, columns, "ENDUSER"),
            BookedPrice = GetDecimal(reader, columns, "BOOKEDPRICE"),
            DateSent = GetDateTime(reader, columns, "DATESENT"),
            DateRequested = GetDateTime(reader, columns, "DATEREQUESTED"),
            ConsigneeRecNo = GetDecimal(reader, columns, "CONSIGNEERECNO"),
            SecondNotifyRecNo = GetDecimal(reader, columns, "SECONDNOTIFYRECNO"),
            BankRecNo = GetDecimal(reader, columns, "BANKRECNO"),
            Seal = GetString(reader, columns, "SEAL"),
            ContactId = GetDecimal(reader, columns, "CONTACTID"),
            PriceBasis = GetDecimal(reader, columns, "PRICEBASIS"),
            Tarped = GetBool(reader, columns, "TARPED"),
            GrossAdjust = GetDecimal(reader, columns, "GROSSADJUST"),
            NetAdjust = GetDecimal(reader, columns, "NETADJUST"),
            Destination = GetString(reader, columns, "DESTINATION"),
            Booking = GetString(reader, columns, "BOOKING"),
            TruckCo = GetString(reader, columns, "TRUCKCO"),
            Msa = GetInt(reader, columns, "MSA"),
            UserId = GetString(reader, columns, "USERID"),
            ChangeId = GetDecimal(reader, columns, "CHANGEID"),
            ChangeSourceId = GetDecimal(reader, columns, "CHANGESOURCEID"),
            ShippingBlackoutDateStart = GetDateTime(reader, columns, "SHIPPINGBLACKOUTDATESTART"),
            ShippingBlackoutDateEnd = GetDateTime(reader, columns, "SHIPPINGBLACKOUTDATEEND"),
            RequestDate = GetDateTime(reader, columns, "REQUESTDATE"),
            FreightForwarder = GetString(reader, columns, "FREIGHTFORWARDER"),
            CustomerFreightDateSent = GetDateTime(reader, columns, "CUSTOMERFREIGHTDATESENT"),
            CustomerFreightDateRequested = GetDateTime(reader, columns, "CUSTOMERFREIGHTDATEREQUESTED"),
            BookingDate = GetDateTime(reader, columns, "BOOKINGDATE"),
            RequestAsap = GetBool(reader, columns, "REQUESTASAP"),
            OnHold = GetBool(reader, columns, "ONHOLD"),
            Pickup = GetBool(reader, columns, "PICKUP"),
            ExternalShipmentId = GetString(reader, columns, "EXTERNALSHIPMENTID"),
            IgnoreForNumberingPurposes = GetBool(reader, columns, "IgnoreForNumberingPurposes"),
            DisplayUnit = GetString(reader, columns, "DISPLAYUNIT"),
            FscType = GetByte(reader, columns, "FSCType"),
            StatusSetBy = GetByte(reader, columns, "STATUS_SET_BY"),
            Distance = GetDecimal(reader, columns, "DISTANCE"),
            TaxRegionId = GetLong(reader, columns, "TaxRegionId"),
            DistanceUnit = GetDecimal(reader, columns, "DistanceUnit"),
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

    private static long GetLong(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToInt64(v) : 0L;

    private static decimal GetDecimal(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToDecimal(v) : 0m;

    private static DateTime GetDateTime(SqlDataReader reader, IReadOnlyDictionary<string, int> columns, string column)
        => TryGetValue(reader, columns, column, out var v) ? Convert.ToDateTime(v) : default;
}
