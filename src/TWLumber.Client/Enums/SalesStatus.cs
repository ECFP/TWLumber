using System.Runtime.Serialization;

namespace TWLumber.Client.Enums;

/// <summary>
/// The status of a sales order, as stored in the Tallyworks <c>SALES.STATUS</c> column and
/// surfaced as <see cref="Models.Sale.Status"/>. Members whose Tallyworks label differs from the
/// member name carry that label in an <see cref="EnumMemberAttribute"/>.
/// <para>
/// Not every numeric value is mapped; a value this enum does not define (for example <c>5</c>)
/// maps to an undefined member rather than throwing, so check
/// <see cref="Enum.IsDefined{TEnum}(TEnum)"/> when the value comes from an untrusted row.
/// </para>
/// </summary>
public enum SalesStatus
{
    /// <summary>Stored as <c>0</c>. The order is open.</summary>
    Open = 0,

    /// <summary>Stored as <c>1</c>. The order is on hold.</summary>
    Held = 1,

    /// <summary>Stored as <c>2</c>. The order was closed.</summary>
    Closed = 2,

    /// <summary>Stored as <c>3</c>. The order was closed by Tallyworks rather than by a user; labelled "Closed - Automatically".</summary>
    [EnumMember(Value = "Closed - Automatically")]
    ClosedAuto = 3,

    /// <summary>Stored as <c>4</c>. The order is a quote rather than a committed sale.</summary>
    Quote = 4,

    /// <summary>Stored as <c>6</c>. The order is ready.</summary>
    Ready = 6,

    /// <summary>Stored as <c>7</c>. The order was cancelled.</summary>
    Cancelled = 7
}
