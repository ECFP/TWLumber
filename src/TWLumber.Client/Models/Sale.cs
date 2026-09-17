using TWLumber.Client.Enums;

namespace TWLumber.Client.Models;

/// <summary>
/// Represents a row in the Tallyworks <c>SALES</c> table (a sales order / quote header).
/// Property names are the idiomatic C# form of the underlying column; the source column
/// name is noted where it is not obvious.
/// </summary>
public class Sale
{
    /// <summary>Identity key. Column <c>RECNO</c> — decimal(10,0).</summary>
    public decimal RecNo { get; set; }

    /// <summary>Column <c>SALESORDER</c> — varchar(30).</summary>
    public string SalesOrder { get; set; } = string.Empty;

    /// <summary>Column <c>ORIGINALSALESORDER</c> — varchar(30).</summary>
    public string OriginalSalesOrder { get; set; } = string.Empty;

    /// <summary>Column <c>CUSTCODE</c> — varchar(10).</summary>
    public string CustCode { get; set; } = string.Empty;

    /// <summary>Column <c>POSORDER</c> — bit.</summary>
    public bool PosOrder { get; set; }

    /// <summary>Column <c>POSCUSTNAME</c> — varchar(64).</summary>
    public string PosCustName { get; set; } = string.Empty;

    /// <summary>Column <c>CUSTORDNUM</c> — varchar(30).</summary>
    public string CustOrdNum { get; set; } = string.Empty;

    /// <summary>Column <c>LOCALCUST</c> — varchar(30).</summary>
    public string LocalCust { get; set; } = string.Empty;

    /// <summary>Column <c>ENTRYDATE</c> — datetime.</summary>
    public DateTime EntryDate { get; set; }

    /// <summary>Column <c>CURRENCY</c> — varchar(3).</summary>
    public string Currency { get; set; } = string.Empty;

    /// <summary>Column <c>PRIORITY</c> — decimal(2,0).</summary>
    public decimal Priority { get; set; }

    /// <summary>Column <c>SALESPERSON</c> — varchar(50).</summary>
    public string SalesPerson { get; set; } = string.Empty;

    /// <summary>Column <c>MARKET</c> — char(4).</summary>
    public string Market { get; set; } = string.Empty;

    /// <summary>Column <c>FOB</c> — varchar(30).</summary>
    public string Fob { get; set; } = string.Empty;

    /// <summary>Column <c>TRANSPORTTYPE</c> — varchar(50).</summary>
    public string TransportType { get; set; } = string.Empty;

    /// <summary>Column <c>SALENOTES</c> — varchar(2000).</summary>
    public string SaleNotes { get; set; } = string.Empty;

    /// <summary>Column <c>MILLNOTES</c> — varchar(2000).</summary>
    public string MillNotes { get; set; } = string.Empty;

    /// <summary>Column <c>CUSTNOTES</c> — varchar(2000).</summary>
    public string CustNotes { get; set; } = string.Empty;

    /// <summary>Column <c>TERMS</c> — varchar(5).</summary>
    public string Terms { get; set; } = string.Empty;

    /// <summary>Column <c>CONTACT</c> — varchar(64).</summary>
    public string Contact { get; set; } = string.Empty;

    /// <summary>Column <c>STATUS</c> — decimal(1,0). Interpreted via <see cref="SalesStatus"/>.</summary>
    public SalesStatus Status { get; set; }

    /// <summary>Column <c>REVISED</c> — datetime.</summary>
    public DateTime Revised { get; set; }

    /// <summary>Column <c>NOEXPORT</c> — bit.</summary>
    public bool NoExport { get; set; }

    /// <summary>Column <c>LOGO</c> — varchar(64).</summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>Column <c>CLASS</c> — varchar(15).</summary>
    public string Class { get; set; } = string.Empty;

    /// <summary>Column <c>CertificationNo</c> — varchar(30).</summary>
    public string CertificationNo { get; set; } = string.Empty;

    /// <summary>Column <c>ITEMTYPE</c> — decimal(1,0).</summary>
    public decimal ItemType { get; set; }

    /// <summary>Column <c>LOCATION</c> — varchar(15).</summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>Column <c>QUOTATYPE</c> — char(1).</summary>
    public string QuotaType { get; set; } = string.Empty;

    /// <summary>Column <c>MARK</c> — varchar(80).</summary>
    public string Mark { get; set; } = string.Empty;

    /// <summary>Column <c>OVERRIDETAXA</c> — bit.</summary>
    public bool OverrideTaxA { get; set; }

    /// <summary>Column <c>OVERRIDETAXB</c> — bit.</summary>
    public bool OverrideTaxB { get; set; }

    /// <summary>Column <c>OVERRIDETAXC</c> — bit.</summary>
    public bool OverrideTaxC { get; set; }

    /// <summary>Column <c>OVERRIDETAXD</c> — bit.</summary>
    public bool OverrideTaxD { get; set; }

    /// <summary>Column <c>OVERRIDETAXAPC</c> — decimal(12,4).</summary>
    public decimal OverrideTaxAPc { get; set; }

    /// <summary>Column <c>OVERRIDETAXBPC</c> — decimal(12,4).</summary>
    public decimal OverrideTaxBPc { get; set; }

    /// <summary>Column <c>OVERRIDETAXCPC</c> — decimal(12,4).</summary>
    public decimal OverrideTaxCPc { get; set; }

    /// <summary>Column <c>OVERRIDETAXDPC</c> — decimal(12,4).</summary>
    public decimal OverrideTaxDPc { get; set; }

    /// <summary>Column <c>TRANSPORTSETUPREQUIRED</c> — decimal(1,0).</summary>
    public decimal TransportSetupRequired { get; set; }

    /// <summary>Column <c>CityID</c> — decimal(8,0).</summary>
    public decimal CityId { get; set; }

    /// <summary>Column <c>InitDateTime</c> — datetime.</summary>
    public DateTime InitDateTime { get; set; }

    /// <summary>Column <c>UNASSIGNED</c> — bit.</summary>
    public bool Unassigned { get; set; }

    /// <summary>Column <c>HIDEQO</c> — bit.</summary>
    public bool HideQo { get; set; }

    /// <summary>Column <c>DESCRIPTION</c> — varchar(2000).</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Column <c>IsAlteringCustOrdnum</c> — decimal(5,0).</summary>
    public decimal IsAlteringCustOrdNum { get; set; }

    /// <summary>Column <c>PrintTagDetail</c> — bit.</summary>
    public bool PrintTagDetail { get; set; }

    /// <summary>Column <c>ORIGINMILL</c> — char(2).</summary>
    public string OriginMill { get; set; } = string.Empty;

    /// <summary>Column <c>DEFAULTQU</c> — char(1).</summary>
    public string DefaultQu { get; set; } = string.Empty;

    /// <summary>Column <c>DEFAULTPU</c> — char(1).</summary>
    public string DefaultPu { get; set; } = string.Empty;

    /// <summary>Column <c>DEFAULTPB</c> — decimal(1,0).</summary>
    public decimal DefaultPb { get; set; }

    /// <summary>Column <c>PARENTSALESORDER</c> — varchar(30).</summary>
    public string ParentSalesOrder { get; set; } = string.Empty;

    /// <summary>Column <c>MASTERORDER</c> — bit.</summary>
    public bool MasterOrder { get; set; }

    /// <summary>Column <c>CPRUNTYPE</c> — decimal(1,0).</summary>
    public decimal CpRunType { get; set; }

    /// <summary>Column <c>CertificationNote</c> — varchar(256).</summary>
    public string CertificationNote { get; set; } = string.Empty;

    /// <summary>Column <c>CertificationPct</c> — decimal(5,2).</summary>
    public decimal CertificationPct { get; set; }

    /// <summary>Column <c>CertificationUnit</c> — varchar(1).</summary>
    public string CertificationUnit { get; set; } = string.Empty;

    /// <summary>Column <c>PRIMARYAPPROVAL</c> — bit.</summary>
    public bool PrimaryApproval { get; set; }

    /// <summary>Column <c>SECONDARYAPPROVAL</c> — bit.</summary>
    public bool SecondaryApproval { get; set; }

    /// <summary>Column <c>PRIMARYAPPROVER</c> — varchar(64).</summary>
    public string PrimaryApprover { get; set; } = string.Empty;

    /// <summary>Column <c>SECONDARYAPPROVER</c> — varchar(64).</summary>
    public string SecondaryApprover { get; set; } = string.Empty;

    /// <summary>Column <c>EDITED</c> — bit.</summary>
    public bool Edited { get; set; }

    /// <summary>Column <c>SALESORDERREVISION</c> — int.</summary>
    public int SalesOrderRevision { get; set; }

    /// <summary>Column <c>IsCertified</c> — bit.</summary>
    public bool IsCertified { get; set; }

    /// <summary>Column <c>CONFIRMATIONPRINTED</c> — bit.</summary>
    public bool ConfirmationPrinted { get; set; }

    /// <summary>Column <c>RECEIVEDBYSHIPPING</c> — bit.</summary>
    public bool ReceivedByShipping { get; set; }

    /// <summary>Column <c>BelowThresholdSale</c> — bit.</summary>
    public bool BelowThresholdSale { get; set; }

    /// <summary>Column <c>TRUCKNUM</c> — varchar(12).</summary>
    public string TruckNum { get; set; } = string.Empty;

    /// <summary>Column <c>USERDEFINED1</c> — varchar(80).</summary>
    public string UserDefined1 { get; set; } = string.Empty;

    /// <summary>Column <c>PERMITNUM</c> — varchar(20).</summary>
    public string PermitNum { get; set; } = string.Empty;

    /// <summary>Column <c>Ready</c> — bit.</summary>
    public bool Ready { get; set; }

    /// <summary>Column <c>ShipToTable</c> — tinyint.</summary>
    public byte ShipToTable { get; set; }

    /// <summary>Column <c>ShipToRecno</c> — decimal(10,0).</summary>
    public decimal ShipToRecNo { get; set; }

    /// <summary>Column <c>PREPAYMENT</c> — decimal(11,2).</summary>
    public decimal Prepayment { get; set; }

    /// <summary>Column <c>CareOfTable</c> — tinyint.</summary>
    public byte CareOfTable { get; set; }

    /// <summary>Column <c>CareOfRecno</c> — decimal(10,0).</summary>
    public decimal CareOfRecNo { get; set; }

    /// <summary>Column <c>ISVMIORDER</c> — bit.</summary>
    public bool IsVmiOrder { get; set; }

    /// <summary>Column <c>FSCCERTIFIED</c> — bit.</summary>
    public bool FscCertified { get; set; }

    /// <summary>Column <c>TARPREQUIRED</c> — bit.</summary>
    public bool TarpRequired { get; set; }

    /// <summary>Column <c>FREIGHT</c> — decimal(12,2).</summary>
    public decimal Freight { get; set; }

    /// <summary>Column <c>AUTOTALLYCOMPLETED</c> — bit.</summary>
    public bool AutoTallyCompleted { get; set; }

    /// <summary>Column <c>MODE</c> — varchar(30).</summary>
    public string Mode { get; set; } = string.Empty;

    /// <summary>Column <c>LUMBERTAG</c> — varchar(3).</summary>
    public string LumberTag { get; set; } = string.Empty;

    /// <summary>Column <c>SALESORDERNEEDSCESOREVIEW</c> — bit.</summary>
    public bool SalesOrderNeedsCesoReview { get; set; }

    /// <summary>Column <c>SENDTOREMANRUNONLY</c> — bit.</summary>
    public bool SendToRemanRunOnly { get; set; }

    /// <summary>Column <c>LinkedItemSetToHoldFromCustomer</c> — bit.</summary>
    public bool LinkedItemSetToHoldFromCustomer { get; set; }

    /// <summary>Column <c>ISCUSTOMSALESORDER</c> — bit.</summary>
    public bool IsCustomSalesOrder { get; set; }

    /// <summary>Column <c>CREATEDBY</c> — varchar(30).</summary>
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>Column <c>LASTEDITEDBY</c> — varchar(30).</summary>
    public string LastEditedBy { get; set; } = string.Empty;

    /// <summary>Column <c>MILLCODE</c> — decimal(3,0).</summary>
    public decimal MillCode { get; set; }

    /// <summary>Column <c>HOTSHIPMENTCOMPLETED</c> — bit.</summary>
    public bool HotShipmentCompleted { get; set; }

    /// <summary>Column <c>VMILocation</c> — varchar(15).</summary>
    public string VmiLocation { get; set; } = string.Empty;

    /// <summary>Column <c>ENDUSERCUSTCODE</c> — varchar(10).</summary>
    public string EndUserCustCode { get; set; } = string.Empty;

    /// <summary>Column <c>CommissionPercentage</c> — decimal(5,2).</summary>
    public decimal CommissionPercentage { get; set; }

    /// <summary>Column <c>SHIPMENTSTATUS</c> — varchar(4). See <see cref="SalesShipmentStatus"/>.</summary>
    public string ShipmentStatus { get; set; } = string.Empty;

    /// <summary>Column <c>Agent</c> — varchar(4).</summary>
    public string Agent { get; set; } = string.Empty;

    /// <summary>Column <c>SOLDBYCUSTCODE</c> — varchar(10).</summary>
    public string SoldByCustCode { get; set; } = string.Empty;

    /// <summary>Column <c>CustGradeMeasure</c> — bit.</summary>
    public bool CustGradeMeasure { get; set; }

    /// <summary>Column <c>CommissionPayBy</c> — int.</summary>
    public int CommissionPayBy { get; set; }

    /// <summary>Column <c>CommissionDollarsPer</c> — decimal(12,4).</summary>
    public decimal CommissionDollarsPer { get; set; }

    /// <summary>Column <c>CommissionUnit</c> — varchar(1).</summary>
    public string CommissionUnit { get; set; } = string.Empty;

    /// <summary>Column <c>PHONE</c> — varchar(20).</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>Column <c>EMAIL</c> — varchar(80).</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>Column <c>SELECTNEWCUSTOMER</c> — tinyint.</summary>
    public byte SelectNewCustomer { get; set; }

    /// <summary>Column <c>CONTAINERNUMBER</c> — varchar(50).</summary>
    public string ContainerNumber { get; set; } = string.Empty;

    /// <summary>Column <c>PORTOFENTRY</c> — varchar(4).</summary>
    public string PortOfEntry { get; set; } = string.Empty;

    /// <summary>Column <c>PORTOFDEST</c> — varchar(4).</summary>
    public string PortOfDest { get; set; } = string.Empty;

    /// <summary>Column <c>EQUIPMENT</c> — varchar(4).</summary>
    public string Equipment { get; set; } = string.Empty;

    /// <summary>Column <c>FILTERTOFOLLOW</c> — bit.</summary>
    public bool FilterToFollow { get; set; }

    /// <summary>Column <c>ENDUSER</c> — bit.</summary>
    public bool EndUser { get; set; }

    /// <summary>Column <c>BOOKEDPRICE</c> — decimal(10,2).</summary>
    public decimal BookedPrice { get; set; }

    /// <summary>Column <c>DATESENT</c> — datetime.</summary>
    public DateTime DateSent { get; set; }

    /// <summary>Column <c>DATEREQUESTED</c> — datetime.</summary>
    public DateTime DateRequested { get; set; }

    /// <summary>Column <c>CONSIGNEERECNO</c> — decimal(10,0).</summary>
    public decimal ConsigneeRecNo { get; set; }

    /// <summary>Column <c>SECONDNOTIFYRECNO</c> — decimal(10,0).</summary>
    public decimal SecondNotifyRecNo { get; set; }

    /// <summary>Column <c>BANKRECNO</c> — decimal(10,0).</summary>
    public decimal BankRecNo { get; set; }

    /// <summary>Column <c>SEAL</c> — varchar(50).</summary>
    public string Seal { get; set; } = string.Empty;

    /// <summary>Column <c>CONTACTID</c> — decimal(8,0).</summary>
    public decimal ContactId { get; set; }

    /// <summary>Column <c>PRICEBASIS</c> — decimal(1,0).</summary>
    public decimal PriceBasis { get; set; }

    /// <summary>Column <c>TARPED</c> — bit.</summary>
    public bool Tarped { get; set; }

    /// <summary>Column <c>GROSSADJUST</c> — decimal(12,4).</summary>
    public decimal GrossAdjust { get; set; }

    /// <summary>Column <c>NETADJUST</c> — decimal(12,4).</summary>
    public decimal NetAdjust { get; set; }

    /// <summary>Column <c>DESTINATION</c> — varchar(80).</summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>Column <c>BOOKING</c> — varchar(200).</summary>
    public string Booking { get; set; } = string.Empty;

    /// <summary>Column <c>TRUCKCO</c> — varchar(50).</summary>
    public string TruckCo { get; set; } = string.Empty;

    /// <summary>Column <c>MSA</c> — int.</summary>
    public int Msa { get; set; }

    /// <summary>Column <c>USERID</c> — varchar(32).</summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>Column <c>CHANGEID</c> — decimal(10,0).</summary>
    public decimal ChangeId { get; set; }

    /// <summary>Column <c>CHANGESOURCEID</c> — decimal(2,0).</summary>
    public decimal ChangeSourceId { get; set; }

    /// <summary>Column <c>SHIPPINGBLACKOUTDATESTART</c> — datetime.</summary>
    public DateTime ShippingBlackoutDateStart { get; set; }

    /// <summary>Column <c>SHIPPINGBLACKOUTDATEEND</c> — datetime.</summary>
    public DateTime ShippingBlackoutDateEnd { get; set; }

    /// <summary>Column <c>REQUESTDATE</c> — datetime.</summary>
    public DateTime RequestDate { get; set; }

    /// <summary>Column <c>FREIGHTFORWARDER</c> — varchar(4).</summary>
    public string FreightForwarder { get; set; } = string.Empty;

    /// <summary>Column <c>CUSTOMERFREIGHTDATESENT</c> — datetime.</summary>
    public DateTime CustomerFreightDateSent { get; set; }

    /// <summary>Column <c>CUSTOMERFREIGHTDATEREQUESTED</c> — datetime.</summary>
    public DateTime CustomerFreightDateRequested { get; set; }

    /// <summary>Column <c>BOOKINGDATE</c> — datetime.</summary>
    public DateTime BookingDate { get; set; }

    /// <summary>Column <c>REQUESTASAP</c> — bit.</summary>
    public bool RequestAsap { get; set; }

    /// <summary>Column <c>ONHOLD</c> — bit.</summary>
    public bool OnHold { get; set; }

    /// <summary>Column <c>PICKUP</c> — bit.</summary>
    public bool Pickup { get; set; }

    /// <summary>Column <c>EXTERNALSHIPMENTID</c> — varchar(16).</summary>
    public string ExternalShipmentId { get; set; } = string.Empty;

    /// <summary>Column <c>IgnoreForNumberingPurposes</c> — bit.</summary>
    public bool IgnoreForNumberingPurposes { get; set; }

    /// <summary>Column <c>DISPLAYUNIT</c> — char(1).</summary>
    public string DisplayUnit { get; set; } = string.Empty;

    /// <summary>Column <c>FSCType</c> — tinyint.</summary>
    public byte FscType { get; set; }

    /// <summary>Column <c>STATUS_SET_BY</c> — tinyint.</summary>
    public byte StatusSetBy { get; set; }

    /// <summary>Column <c>DISTANCE</c> — decimal(8,2).</summary>
    public decimal Distance { get; set; }

    /// <summary>Column <c>TaxRegionId</c> — bigint.</summary>
    public long TaxRegionId { get; set; }

    /// <summary>Column <c>DistanceUnit</c> — decimal(2,0).</summary>
    public decimal DistanceUnit { get; set; }

    /// <summary>
    /// The line items belonging to this sale (linked by <see cref="SalesOrder"/>).
    /// Populated only when the sale is retrieved with <c>includeItems: true</c>;
    /// otherwise empty.
    /// </summary>
    public IReadOnlyList<SaleItem> SaleItems { get; set; } = Array.Empty<SaleItem>();
}
