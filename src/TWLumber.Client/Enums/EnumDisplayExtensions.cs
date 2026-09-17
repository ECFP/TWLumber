using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.Serialization;

namespace TWLumber.Client.Enums;

/// <summary>
/// Extensions for turning enum values into the labels Tallyworks displays for them.
/// </summary>
public static class EnumDisplayExtensions
{
    private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, string>> LabelsByType = new();

    /// <summary>
    /// Gets the display label for an enum value: the <see cref="EnumMemberAttribute.Value"/> when the
    /// member declares one, otherwise the member name.
    /// <para>
    /// A value with no matching member (for example a status code this library does not map) has no
    /// label, so its numeric value is returned as text.
    /// </para>
    /// </summary>
    /// <param name="value">The enum value to label, e.g. <see cref="SalesStatus.ClosedAuto"/>.</param>
    /// <returns>
    /// The label, e.g. <c>"Closed - Automatically"</c> for <see cref="SalesStatus.ClosedAuto"/>
    /// and <c>"Open"</c> for <see cref="SalesStatus.Open"/>.
    /// </returns>
    public static string ToDisplayName<TEnum>(this TEnum value) where TEnum : struct, Enum
    {
        var name = value.ToString();
        var labels = LabelsByType.GetOrAdd(typeof(TEnum), static type => BuildLabels(type));

        return labels.TryGetValue(name, out var label) ? label : name;
    }

    /// <summary>
    /// Reads the <see cref="EnumMemberAttribute"/> off each member of an enum type once, so the
    /// reflection cost is paid a single time per type rather than per call.
    /// </summary>
    private static IReadOnlyDictionary<string, string> BuildLabels(Type enumType)
    {
        var labels = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var field in enumType.GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var value = field.GetCustomAttribute<EnumMemberAttribute>()?.Value;
            if (!string.IsNullOrWhiteSpace(value))
            {
                labels[field.Name] = value;
            }
        }

        return labels;
    }
}
