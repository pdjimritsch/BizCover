using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BizCover.Cars.Common;

[Serializable, ImmutableObject(true)]
public sealed partial class StringToDecimalConverter : JsonConverter<decimal>
{
    #region Overrides

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            try
            {
                var content = reader.GetString();

                if (!string.IsNullOrEmpty(content) && decimal.TryParse(content.Trim(), out var value))
                {
                    return value;
                }
            }
            catch (Exception)
            {
            }
        }

        return decimal.MinValue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    #endregion
}
