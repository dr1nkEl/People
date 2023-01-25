using System.Text.Json;
using System.Text.Json.Serialization;

namespace People.Web.Infrastructure.Converters;

/// <summary>
/// DateOnly json converter.
/// </summary>
public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string serializationFormat;

    /// <summary>
    /// Constructor.
    /// </summary>
    public DateOnlyConverter() : this(null)
    {
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="serializationFormat">Serialization format.</param>
    public DateOnlyConverter(string serializationFormat)
    {
        this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    /// <summary>
    /// Read method.
    /// </summary>
    /// <param name="reader">Reader.</param>
    /// <param name="typeToConvert">Convert to type.</param>
    /// <param name="options">Options.</param>
    /// <returns>DateOnly.</returns>
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value);
    }

    /// <summary>
    /// Write method.
    /// </summary>
    /// <param name="writer">Writer.</param>
    /// <param name="value">Value.</param>
    /// <param name="options">Option.</param>
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(serializationFormat));
    }
}
