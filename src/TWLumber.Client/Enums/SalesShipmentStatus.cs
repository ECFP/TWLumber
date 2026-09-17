using System.Runtime.Serialization;

namespace TWLumber.Client.Enums;

/// <summary>
/// The shipment status of a sales order, describing the values Tallyworks stores in the
/// <c>SALES.SHIPMENTSTATUS</c> column. Members whose Tallyworks label differs from the member name
/// carry that label in an <see cref="EnumMemberAttribute"/>.
/// <para>
/// Note that <see cref="Models.Sale.ShipmentStatus"/> is mapped as the raw <see cref="string"/>
/// from the database, not as this enum; use this type as the reference for what those values mean.
/// Values below <c>3</c> are not mapped here.
/// </para>
/// </summary>
public enum SalesShipmentStatus
{
    /// <summary>Stored as <c>3</c>. The order has been dispatched.</summary>
    Dispatched = 3,

    /// <summary>Stored as <c>4</c>. The order has been partly dispatched; labelled "Dispatched Partial".</summary>
    [EnumMember(Value = "Dispatched Partial")]
    DispatchedPartial = 4,

    /// <summary>Stored as <c>5</c>. The order is offsite.</summary>
    Offsite = 5,

    /// <summary>Stored as <c>6</c>. The shipment is on hold.</summary>
    Held = 6
}
