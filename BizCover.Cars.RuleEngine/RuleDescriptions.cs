using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BizCover.Cars.RuleEngine;

/// <summary>
/// 
/// </summary>
[Serializable, ImmutableObject(true)]
public partial class RuleDescriptions
{
    #region Properties

    [JsonInclude]
    [JsonPropertyName("rules")]
    [JsonPropertyOrder(0)]
    public List<RuleDescription> List { get; set; } = [];

    #endregion
}
