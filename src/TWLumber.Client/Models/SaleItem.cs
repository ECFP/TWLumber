namespace TWLumber.Client.Models;

/// <summary>
/// Represents a row in the Tallyworks <c>SALEITEMS</c> table (a line item on a sales order).
/// Property names are the idiomatic C# form of the underlying column; the source column
/// name is noted where it is not obvious.
/// </summary>
public class SaleItem
{
    /// <summary>Column <c>SALESORDER</c> — varchar(30). Part of the unique key with <see cref="SaleItemNumber"/>.</summary>
    public string SalesOrder { get; set; } = string.Empty;

    /// <summary>Column <c>SALEITEM</c> — varchar(30). Part of the unique key with <see cref="SalesOrder"/>.</summary>
    public string SaleItemNumber { get; set; } = string.Empty;

    /// <summary>Column <c>PRSALEITEM</c> — varchar(300).</summary>
    public string PrSaleItem { get; set; } = string.Empty;

    /// <summary>Column <c>TYPE</c> — decimal(1,0).</summary>
    public decimal Type { get; set; }

    /// <summary>Column <c>DESCRIPTION</c> — varchar(2000).</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>Column <c>QUANTITY</c> — decimal(13,4).</summary>
    public decimal Quantity { get; set; }

    /// <summary>Column <c>QUANTITYUNIT</c> — char(1).</summary>
    public string QuantityUnit { get; set; } = string.Empty;

    /// <summary>Column <c>PRICEUNIT</c> — char(1).</summary>
    public string PriceUnit { get; set; } = string.Empty;

    /// <summary>Column <c>DISPLAYUNIT</c> — char(1).</summary>
    public string DisplayUnit { get; set; } = string.Empty;

    /// <summary>Column <c>DISCOUNT</c> — decimal(6,2).</summary>
    public decimal Discount { get; set; }

    /// <summary>Column <c>PRICEBASIS</c> — decimal(1,0).</summary>
    public decimal PriceBasis { get; set; }

    /// <summary>Column <c>TRUCKCO</c> — varchar(50).</summary>
    public string TruckCo { get; set; } = string.Empty;

    /// <summary>Column <c>READYDATE</c> — datetime.</summary>
    public DateTime ReadyDate { get; set; }

    /// <summary>Column <c>SHIPSTARTDATE</c> — datetime.</summary>
    public DateTime ShipStartDate { get; set; }

    /// <summary>Column <c>SHIPENDDATE</c> — datetime.</summary>
    public DateTime ShipEndDate { get; set; }

    /// <summary>Column <c>SHIPTO</c> — varchar(2000).</summary>
    public string ShipTo { get; set; } = string.Empty;

    /// <summary>Column <c>CAREOF</c> — varchar(2000).</summary>
    public string CareOf { get; set; } = string.Empty;

    /// <summary>Column <c>DESTINATION</c> — varchar(80).</summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>Column <c>DOCK</c> — varchar(25).</summary>
    public string Dock { get; set; } = string.Empty;

    /// <summary>Column <c>VESSEL</c> — varchar(30).</summary>
    public string Vessel { get; set; } = string.Empty;

    /// <summary>Column <c>VOYAGE</c> — varchar(32).</summary>
    public string Voyage { get; set; } = string.Empty;

    /// <summary>Column <c>BOOKING</c> — varchar(200).</summary>
    public string Booking { get; set; } = string.Empty;

    /// <summary>Column <c>ROUTE</c> — varchar(128).</summary>
    public string Route { get; set; } = string.Empty;

    /// <summary>Column <c>PRODNOTES</c> — varchar(2000).</summary>
    public string ProdNotes { get; set; } = string.Empty;

    /// <summary>Column <c>MORINET</c> — char(1).</summary>
    public string MoriNet { get; set; } = string.Empty;

    /// <summary>Column <c>ALLOWTHICKER</c> — bit.</summary>
    public bool AllowThicker { get; set; }

    /// <summary>Column <c>ALLOWWIDER</c> — bit.</summary>
    public bool AllowWider { get; set; }

    /// <summary>Column <c>WANE</c> — varchar(10).</summary>
    public string Wane { get; set; } = string.Empty;

    /// <summary>Column <c>WRAP</c> — char(1).</summary>
    public string Wrap { get; set; } = string.Empty;

    /// <summary>Column <c>ANTISTAIN</c> — char(1).</summary>
    public string AntiStain { get; set; } = string.Empty;

    /// <summary>Column <c>ENDSEAL</c> — char(2).</summary>
    public string EndSeal { get; set; } = string.Empty;

    /// <summary>Column <c>PACKAGING</c> — char(1).</summary>
    public string Packaging { get; set; } = string.Empty;

    /// <summary>Column <c>PACKAGESIZE</c> — char(1).</summary>
    public string PackageSize { get; set; } = string.Empty;

    /// <summary>Column <c>LABELNAME</c> — varchar(20).</summary>
    public string LabelName { get; set; } = string.Empty;

    /// <summary>Column <c>LABELCOUNT</c> — decimal(2,0).</summary>
    public decimal LabelCount { get; set; }

    /// <summary>Column <c>LABELPRINTER</c> — varchar(256).</summary>
    public string LabelPrinter { get; set; } = string.Empty;

    /// <summary>Column <c>LOGO</c> — varchar(20).</summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>Column <c>RUNLINK</c> — varchar(30).</summary>
    public string RunLink { get; set; } = string.Empty;

    /// <summary>Column <c>RUNITEMLINK</c> — varchar(30).</summary>
    public string RunItemLink { get; set; } = string.Empty;

    /// <summary>Column <c>ALLOWINCREMENT</c> — bit.</summary>
    public bool AllowIncrement { get; set; }

    /// <summary>Column <c>LOCATION</c> — varchar(15).</summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>Column <c>GRADERULE</c> — char(1).</summary>
    public string GradeRule { get; set; } = string.Empty;

    /// <summary>Column <c>SPECIES</c> — varchar(30).</summary>
    public string Species { get; set; } = string.Empty;

    /// <summary>Column <c>GROSSADJUST</c> — decimal(12,4).</summary>
    public decimal GrossAdjust { get; set; }

    /// <summary>Column <c>NETADJUST</c> — decimal(12,4).</summary>
    public decimal NetAdjust { get; set; }

    /// <summary>Column <c>PRIORITY</c> — decimal(10,0).</summary>
    public decimal Priority { get; set; }

    /// <summary>Column <c>LOT</c> — varchar(10).</summary>
    public string Lot { get; set; } = string.Empty;

    /// <summary>Column <c>CONTRACT</c> — varchar(30).</summary>
    public string Contract { get; set; } = string.Empty;

    /// <summary>Column <c>ALLOWLONGER</c> — bit.</summary>
    public bool AllowLonger { get; set; }

    /// <summary>Column <c>QUANTITYI</c> — decimal(13,4).</summary>
    public decimal QuantityI { get; set; }

    /// <summary>Column <c>QUANTITYM</c> — decimal(13,4).</summary>
    public decimal QuantityM { get; set; }

    /// <summary>Column <c>QUANTITYK</c> — decimal(13,4).</summary>
    public decimal QuantityK { get; set; }

    /// <summary>Column <c>QUANTITYP</c> — decimal(13,4).</summary>
    public decimal QuantityP { get; set; }

    /// <summary>Column <c>QUANTITYL</c> — decimal(13,4).</summary>
    public decimal QuantityL { get; set; }

    /// <summary>Column <c>QUANTITYA</c> — decimal(13,4).</summary>
    public decimal QuantityA { get; set; }

    /// <summary>Column <c>QUANTITYB</c> — decimal(13,4).</summary>
    public decimal QuantityB { get; set; }

    /// <summary>Column <c>QUANTITYT</c> — decimal(13,4).</summary>
    public decimal QuantityT { get; set; }

    /// <summary>Column <c>GENERATEDDESC</c> — varchar(2000).</summary>
    public string GeneratedDesc { get; set; } = string.Empty;

    /// <summary>Column <c>COLOURSPECIFICATION</c> — char(4).</summary>
    public string ColourSpecification { get; set; } = string.Empty;

    /// <summary>Column <c>WIDTHSPECIFICATION</c> — char(4).</summary>
    public string WidthSpecification { get; set; } = string.Empty;

    /// <summary>Column <c>LENGTHSPECIFICATION</c> — char(4).</summary>
    public string LengthSpecification { get; set; } = string.Empty;

    /// <summary>Column <c>MOISTURECONTENT</c> — char(4).</summary>
    public string MoistureContent { get; set; } = string.Empty;

    /// <summary>Column <c>TRANSPORTTYPE</c> — varchar(50).</summary>
    public string TransportType { get; set; } = string.Empty;

    /// <summary>Column <c>PAYMENTMETHOD</c> — char(4).</summary>
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>Column <c>HASLOGO</c> — bit.</summary>
    public bool HasLogo { get; set; }

    /// <summary>Column <c>HASDETAIL</c> — decimal(1,0).</summary>
    public decimal HasDetail { get; set; }

    /// <summary>Column <c>OFFPRODUCTSPEC</c> — bit.</summary>
    public bool OffProductSpec { get; set; }

    /// <summary>Column <c>SOURCERUNITEM</c> — varchar(30).</summary>
    public string SourceRunItem { get; set; } = string.Empty;

    /// <summary>Column <c>YardNote</c> — varchar(2000).</summary>
    public string YardNote { get; set; } = string.Empty;

    /// <summary>Column <c>ROUTINGID</c> — int, nullable. FK to <c>ROUTINGS.RoutingID</c>.</summary>
    public int? RoutingId { get; set; }

    /// <summary>Column <c>prodnoteID</c> — decimal(10,0).</summary>
    public decimal ProdNoteId { get; set; }

    /// <summary>Column <c>yardnoteID</c> — decimal(10,0).</summary>
    public decimal YardNoteId { get; set; }

    /// <summary>Column <c>IsFlexItem</c> — bit.</summary>
    public bool IsFlexItem { get; set; }

    /// <summary>Column <c>Tarped</c> — bit.</summary>
    public bool Tarped { get; set; }

    /// <summary>Identity key. Column <c>RECNO</c> — decimal(10,0).</summary>
    public decimal RecNo { get; set; }

    /// <summary>Column <c>QUANTITYN</c> — decimal(13,4).</summary>
    public decimal QuantityN { get; set; }

    /// <summary>Column <c>SAILINGDATE</c> — datetime.</summary>
    public DateTime SailingDate { get; set; }

    /// <summary>Column <c>ARRIVALDATE</c> — datetime.</summary>
    public DateTime ArrivalDate { get; set; }

    /// <summary>Column <c>PRICINGDATE</c> — datetime.</summary>
    public DateTime PricingDate { get; set; }

    /// <summary>Column <c>PriceBreakdownSetting</c> — tinyint.</summary>
    public byte PriceBreakdownSetting { get; set; }

    /// <summary>Column <c>ORIGINALSALESORDER</c> — varchar(30).</summary>
    public string OriginalSalesOrder { get; set; } = string.Empty;

    /// <summary>Column <c>PTS</c> — bit.</summary>
    public bool Pts { get; set; }

    /// <summary>Column <c>FORMULACODE</c> — varchar(8).</summary>
    public string FormulaCode { get; set; } = string.Empty;

    /// <summary>Column <c>PRICETABLE</c> — varchar(16).</summary>
    public string PriceTable { get; set; } = string.Empty;

    /// <summary>Column <c>READY</c> — bit.</summary>
    public bool Ready { get; set; }

    /// <summary>Column <c>PRODUCTLEVEL</c> — bit.</summary>
    public bool ProductLevel { get; set; }

    /// <summary>Column <c>SHIPTOCUSTCODE</c> — varchar(10).</summary>
    public string ShipToCustCode { get; set; } = string.Empty;

    /// <summary>Column <c>CAREOFCUSTCODE</c> — varchar(10).</summary>
    public string CareOfCustCode { get; set; } = string.Empty;

    /// <summary>Column <c>PORTOFLOADING</c> — varchar(100).</summary>
    public string PortOfLoading { get; set; } = string.Empty;

    /// <summary>Column <c>PORTOFLANDING</c> — varchar(100).</summary>
    public string PortOfLanding { get; set; } = string.Empty;

    /// <summary>Column <c>INSUREDAMOUNT</c> — decimal(13,2).</summary>
    public decimal InsuredAmount { get; set; }

    /// <summary>Column <c>PULLEDITEMREADY</c> — bit.</summary>
    public bool PulledItemReady { get; set; }

    /// <summary>Column <c>HEATTREATMENT</c> — varchar(4).</summary>
    public string HeatTreatment { get; set; } = string.Empty;

    /// <summary>Column <c>MATERIALFROM</c> — decimal(1,0).</summary>
    public decimal MaterialFrom { get; set; }

    /// <summary>Column <c>UserDefinedSort</c> — int.</summary>
    public int UserDefinedSort { get; set; }

    /// <summary>Column <c>RECORDSOURCE</c> — int.</summary>
    public int RecordSource { get; set; }

    /// <summary>Column <c>SENDTOPRODUCTIONPLAN</c> — bit.</summary>
    public bool SendToProductionPlan { get; set; }

    /// <summary>Column <c>PULLEDDATE</c> — datetime.</summary>
    public DateTime PulledDate { get; set; }

    /// <summary>Column <c>FSCCERTIFIED</c> — bit.</summary>
    public bool FscCertified { get; set; }

    /// <summary>Column <c>UserDefinedType</c> — varchar(100).</summary>
    public string UserDefinedType { get; set; } = string.Empty;

    /// <summary>Column <c>UserDefinedDescription</c> — varchar(100).</summary>
    public string UserDefinedDescription { get; set; } = string.Empty;

    /// <summary>Column <c>CUSTOMUNIT</c> — char(1).</summary>
    public string CustomUnit { get; set; } = string.Empty;

    /// <summary>Column <c>WEIGHT</c> — decimal(13,4).</summary>
    public decimal Weight { get; set; }

    /// <summary>Column <c>CutPart</c> — varchar(30).</summary>
    public string CutPart { get; set; } = string.Empty;

    /// <summary>Column <c>LENGTHGROUPS</c> — varchar(150).</summary>
    public string LengthGroups { get; set; } = string.Empty;

    /// <summary>Column <c>PAINTCOLOR</c> — varchar(5).</summary>
    public string PaintColor { get; set; } = string.Empty;

    // -------------------------------------------------------------------------
    // Unit-of-measure aliases. Each is a friendly name for one of the QUANTITY*
    // columns above; reading or writing an alias reads or writes the same value.
    // -------------------------------------------------------------------------

    /// <summary>Alias for <see cref="QuantityP"/> — piece count.</summary>
    public decimal Pieces { get => QuantityP; set => QuantityP = value; }

    /// <summary>Alias for <see cref="QuantityI"/> — board feet.</summary>
    public decimal BF { get => QuantityI; set => QuantityI = value; }

    /// <summary>Alias for <see cref="QuantityM"/> — cubic metres (m³).</summary>
    public decimal M3 { get => QuantityM; set => QuantityM = value; }

    /// <summary>Alias for <see cref="QuantityK"/> — units.</summary>
    public decimal Units { get => QuantityK; set => QuantityK = value; }

    /// <summary>Alias for <see cref="QuantityL"/> — linear feet.</summary>
    public decimal LF { get => QuantityL; set => QuantityL = value; }

    /// <summary>Alias for <see cref="QuantityA"/> — square feet.</summary>
    public decimal SqFt { get => QuantityA; set => QuantityA = value; }

    /// <summary>Alias for <see cref="QuantityB"/> — square metres (m²).</summary>
    public decimal M2 { get => QuantityB; set => QuantityB = value; }

    /// <summary>Alias for <see cref="QuantityT"/> — tons.</summary>
    public decimal Tons { get => QuantityT; set => QuantityT = value; }

    /// <summary>
    /// The product-level detail rows belonging to this sale item (linked by
    /// <see cref="SalesOrder"/> + <see cref="SaleItemNumber"/>). Populated whenever the sale item
    /// is loaded; otherwise empty.
    /// </summary>
    public IReadOnlyList<SaleItemDetail> SaleItemDetails { get; set; } = Array.Empty<SaleItemDetail>();
}
