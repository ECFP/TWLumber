namespace TWLumber.Client.Models;

/// <summary>
/// Represents a row in the Tallyworks <c>SALEITEMDETAIL</c> table (a product-level detail line
/// beneath a <see cref="SaleItem"/>). Links to its parent sale via <see cref="SalesOrder"/> and to
/// its parent sale item via <see cref="SalesOrder"/> + <see cref="SaleItemNumber"/>.
/// Property names are the idiomatic C# form of the underlying column; the source column name is
/// noted where it is not obvious.
/// </summary>
public class SaleItemDetail
{
    /// <summary>Column <c>SALESORDER</c> — varchar(30). Part of the unique key; links to the parent sale.</summary>
    public string SalesOrder { get; set; } = string.Empty;

    /// <summary>Column <c>ORIGINALSALESORDER</c> — varchar(30).</summary>
    public string OriginalSalesOrder { get; set; } = string.Empty;

    /// <summary>Column <c>SALEITEM</c> — varchar(30). Part of the unique key; links to the parent sale item.</summary>
    public string SaleItemNumber { get; set; } = string.Empty;

    /// <summary>Column <c>PRODUCT</c> — varchar(30). Part of the unique key with <see cref="SalesOrder"/> and <see cref="SaleItemNumber"/>.</summary>
    public string Product { get; set; } = string.Empty;

    /// <summary>Column <c>SPECIES</c> — varchar(30).</summary>
    public string Species { get; set; } = string.Empty;

    /// <summary>Column <c>THICKNET</c> — varchar(9).</summary>
    public string ThickNet { get; set; } = string.Empty;

    /// <summary>Column <c>WIDTHNET</c> — varchar(9).</summary>
    public string WidthNet { get; set; } = string.Empty;

    /// <summary>Column <c>LENGTHNET</c> — decimal(7,4).</summary>
    public decimal LengthNet { get; set; }

    /// <summary>Column <c>GRADECODE</c> — varchar(30).</summary>
    public string GradeCode { get; set; } = string.Empty;

    /// <summary>Column <c>GRAIN</c> — varchar(30).</summary>
    public string Grain { get; set; } = string.Empty;

    /// <summary>Column <c>PIECECHAR1</c> — varchar(30).</summary>
    public string PieceChar1 { get; set; } = string.Empty;

    /// <summary>Column <c>PIECECHAR2</c> — varchar(30).</summary>
    public string PieceChar2 { get; set; } = string.Empty;

    /// <summary>Column <c>STATE</c> — varchar(30).</summary>
    public string State { get; set; } = string.Empty;

    /// <summary>Column <c>PLANING</c> — varchar(30).</summary>
    public string Planing { get; set; } = string.Empty;

    /// <summary>Column <c>PKGCHAR1</c> — varchar(30).</summary>
    public string PkgChar1 { get; set; } = string.Empty;

    /// <summary>Column <c>PKGCHAR2</c> — varchar(30).</summary>
    public string PkgChar2 { get; set; } = string.Empty;

    /// <summary>Column <c>QUANTITY</c> — decimal(13,4).</summary>
    public decimal Quantity { get; set; }

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

    /// <summary>Column <c>QUANTITYN</c> — decimal(13,4).</summary>
    public decimal QuantityN { get; set; }

    /// <summary>Column <c>QUANTITYT</c> — decimal(13,4).</summary>
    public decimal QuantityT { get; set; }

    /// <summary>Column <c>PRICE</c> — decimal(12,4).</summary>
    public decimal Price { get; set; }

    /// <summary>Column <c>GROSSPRICEAMOUNT</c> — decimal(12,4).</summary>
    public decimal GrossPriceAmount { get; set; }

    /// <summary>Column <c>NETMILLVALUE</c> — decimal(12,4).</summary>
    public decimal NetMillValue { get; set; }

    /// <summary>Column <c>CHARGES_INCL</c> — decimal(12,4).</summary>
    public decimal ChargesIncl { get; set; }

    /// <summary>Column <c>PRODUCTPRICE</c> — decimal(15,4).</summary>
    public decimal ProductPrice { get; set; }

    /// <summary>Column <c>ALLOCATEDQUANTITY</c> — decimal(13,4).</summary>
    public decimal AllocatedQuantity { get; set; }

    /// <summary>Column <c>NoteID</c> — decimal(10,0).</summary>
    public decimal NoteId { get; set; }

    /// <summary>Column <c>ORIGINALGROSSPRICEAMOUNT</c> — decimal(12,4).</summary>
    public decimal OriginalGrossPriceAmount { get; set; }

    /// <summary>Column <c>RPCCOST</c> — decimal(12,4).</summary>
    public decimal RpcCost { get; set; }

    /// <summary>Column <c>AUTOGENRECORD</c> — bit.</summary>
    public bool AutoGenRecord { get; set; }

    /// <summary>Identity key. Column <c>RECNO</c> — decimal(10,0).</summary>
    public decimal RecNo { get; set; }

    /// <summary>Column <c>sendtoproductionplan</c> — bit.</summary>
    public bool SendToProductionPlan { get; set; }

    /// <summary>Column <c>JagPackagesOrdered</c> — bit.</summary>
    public bool JagPackagesOrdered { get; set; }

    /// <summary>Column <c>CUSTOMQUANTITY</c> — decimal(13,4).</summary>
    public decimal CustomQuantity { get; set; }

    /// <summary>Column <c>WEIGHT</c> — decimal(13,4).</summary>
    public decimal Weight { get; set; }

    /// <summary>Column <c>EXTERNCODE</c> — varchar(15).</summary>
    public string ExternCode { get; set; } = string.Empty;

    /// <summary>Column <c>PTS</c> — bit.</summary>
    public bool Pts { get; set; }

    /// <summary>Column <c>FORMULACODE</c> — varchar(8).</summary>
    public string FormulaCode { get; set; } = string.Empty;

    /// <summary>Column <c>MARKETPRICE</c> — decimal(12,4).</summary>
    public decimal MarketPrice { get; set; }

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
}
