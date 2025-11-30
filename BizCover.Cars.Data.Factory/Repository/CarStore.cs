using BizCover.Cars.Models;
using BizCover.Cars.Models.Abstraction;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BizCover.Cars.Data.Factory.Repository;

/// <summary>
/// 
/// </summary>
[Serializable, ImmutableObject(true)]
public sealed partial class CarStore
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("dictionary")]
    [JsonPropertyOrder(0)]
    public List<Car> Cars { get; set; } = [];

    #endregion

    #region Functions

    /// <summary>
    /// Generates an enumerable sequence from the vehicle dictionary
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ICar> GetSequence()
    {
        if (Cars.Count == 0)
        {
            yield break;
        }

        foreach (var entry in Cars)
        {
            yield return entry;
        }
    }

    #endregion
}
