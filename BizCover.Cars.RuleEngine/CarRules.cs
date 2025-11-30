using System.ComponentModel;
using BizCover.Cars.Models;

namespace BizCover.Cars.RuleEngine;

[Serializable, ImmutableObject(true)] public sealed partial class CarRules
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly List<RuleDescription> _descriptions;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    public CarRules(RuleRepository? repository) : base()
    {
        repository?.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content"), "Car.Rules.json");

        _descriptions = repository?.List ?? [];
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public List<RuleDescription> RuleDescriptions => _descriptions;

    #endregion
}
