using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BizCover.Cars.RuleEngine;

/// <summary>
/// 
/// </summary>
[Serializable, ImmutableObject(true)]
public partial class RuleDescription
{
    #region Properties

    /// <summary>
    /// Discount ruke name
    /// </summary>
    [JsonPropertyName("name")]
    [JsonInclude]
    [JsonPropertyOrder(0)]
    public string Name { get; set; } = "";


    /// <summary>
    /// Manages the application of the discount rule (apply or remove)
    /// </summary>
    [JsonPropertyName("apply")]
    [JsonInclude]
    [JsonPropertyOrder(1)]
    public bool Apply { get; set; } = true;

    /// <summary>
    /// Effective discount rule indicatir
    /// </summary>
    [JsonPropertyName("enabled")]
    [JsonInclude]
    [JsonPropertyOrder(2)]
    public bool Enabled { get; set; } = false;

    #endregion
}
