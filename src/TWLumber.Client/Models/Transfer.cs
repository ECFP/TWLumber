namespace TWLumber.Client.Models;

/// <summary>
/// Represents a row in the Tallyworks <c>TRANSFERS</c> table (a movement of material between
/// locations, optionally tied to a sales order). Property names are the idiomatic C# form of the
/// underlying column; the source column name is noted where it is not obvious.
/// </summary>
public class Transfer
{
    /// <summary>Primary key. Column <c>TRANSFERID</c> — decimal(8,0).</summary>
    public decimal TransferId { get; set; }

    /// <summary>Column <c>TRANSDATE</c> — datetime.</summary>
    public DateTime TransDate { get; set; }

    /// <summary>Column <c>FROMLOCATION</c> — varchar(15).</summary>
    public string FromLocation { get; set; } = string.Empty;

    /// <summary>Column <c>TOLOCATION</c> — varchar(15).</summary>
    public string ToLocation { get; set; } = string.Empty;

    /// <summary>Column <c>ALLEY</c> — char(4).</summary>
    public string Alley { get; set; } = string.Empty;

    /// <summary>Column <c>OPERATOR</c> — varchar(15).</summary>
    public string Operator { get; set; } = string.Empty;

    /// <summary>Column <c>TRUCKCO</c> — varchar(50).</summary>
    public string TruckCo { get; set; } = string.Empty;

    /// <summary>Column <c>TRUCKNUM</c> — varchar(12).</summary>
    public string TruckNum { get; set; } = string.Empty;

    /// <summary>Column <c>DRIVER</c> — varchar(20).</summary>
    public string Driver { get; set; } = string.Empty;

    /// <summary>Column <c>WEIGHT</c> — decimal(10,3).</summary>
    public decimal Weight { get; set; }

    /// <summary>Column <c>NOTES</c> — varchar(1000).</summary>
    public string Notes { get; set; } = string.Empty;

    /// <summary>Column <c>EXPORTSTATE</c> — decimal(1,0).</summary>
    public decimal ExportState { get; set; }

    /// <summary>Column <c>PERMITNUM</c> — varchar(20).</summary>
    public string PermitNum { get; set; } = string.Empty;

    /// <summary>Column <c>BORDERDATE</c> — datetime.</summary>
    public DateTime BorderDate { get; set; }

    /// <summary>Column <c>CONTAINERNUMBER</c> — varchar(16).</summary>
    public string ContainerNumber { get; set; } = string.Empty;

    /// <summary>Column <c>SEALNUMBER</c> — varchar(50).</summary>
    public string SealNumber { get; set; } = string.Empty;

    /// <summary>Identity key. Column <c>RECNO</c> — decimal(10,0).</summary>
    public decimal RecNo { get; set; }

    /// <summary>Column <c>RUN</c> — varchar(30).</summary>
    public string Run { get; set; } = string.Empty;

    /// <summary>Column <c>SALESORDER</c> — varchar(30). Links to the parent sale.</summary>
    public string SalesOrder { get; set; } = string.Empty;

    /// <summary>Column <c>CUSTORDNUM</c> — varchar(30).</summary>
    public string CustOrdNum { get; set; } = string.Empty;

    /// <summary>Column <c>MILLCODE</c> — decimal(3,0).</summary>
    public decimal MillCode { get; set; }

    /// <summary>Column <c>BOOKING</c> — varchar(200).</summary>
    public string Booking { get; set; } = string.Empty;

    /// <summary>Column <c>BILLTO</c> — varchar(1000).</summary>
    public string BillTo { get; set; } = string.Empty;

    /// <summary>Column <c>BILLTORECNO</c> — decimal(10,0).</summary>
    public decimal BillToRecNo { get; set; }
}
