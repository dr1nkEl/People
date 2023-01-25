using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace People.Infrastructure.DataAccess.Extensions;

/// <summary>
/// Extensions for value convertors.
/// </summary>
public static class ValueConversionExtensions
{
    /// <summary>
    /// Add a JSON value convertor.
    /// </summary>
    /// <typeparam name="T">Property type.</typeparam>
    /// <param name="propertyBuilder">Property builder.</param>
    public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder)
    {
        var jsonSerializerOptions = new JsonSerializerOptions();

        var converter = new ValueConverter<T, string>(
            obj => JsonSerializer.Serialize(obj, jsonSerializerOptions),
            json => JsonSerializer.Deserialize<T>(json, jsonSerializerOptions));

        var comparer = new ValueComparer<T>(
            equalsExpression: (l, r) => JsonSerializer.Serialize(l, jsonSerializerOptions) == JsonSerializer.Serialize(r, jsonSerializerOptions),
            hashCodeExpression: v => v == null ? 0 : JsonSerializer.Serialize(v, jsonSerializerOptions).GetHashCode(),
            snapshotExpression: v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, jsonSerializerOptions), jsonSerializerOptions));

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);

        return propertyBuilder;
    }
}
