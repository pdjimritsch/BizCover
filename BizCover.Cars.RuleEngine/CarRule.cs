using System.ComponentModel;
using System.Reflection;

using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.RuleEngine;

[Serializable, ImmutableObject(true)] public sealed partial class CarRule
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly Rule<ICar>? _constraint;

    /// <summary>
    /// 
    /// </summary>
    private readonly Rule<IQueryable<ICar>>? _constraints;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="description"></param>
    public CarRule(RuleDescription? description, IEnumerable<ICar>? cars) : base()
    {
        _constraint = null;

        if (cars != null && cars.Any() && 
            (description != null) && !string.IsNullOrEmpty(description.Name))
        {
            var exports = Assembly.GetExecutingAssembly().GetExportedTypes();
            
            foreach (var export in exports)
            {
                if (export.IsClass && export.IsAssignableTo(typeof(ICar)) && 
                    export.Name.Equals(description.Name, StringComparison.OrdinalIgnoreCase))
                {
                    if (description.Name.Equals(nameof(CarVolumeDiscountRule), StringComparison.OrdinalIgnoreCase))
                    {
                        _constraints = new CarVolumeDiscountRule(cars.AsQueryable(), description.Apply, description.Enabled);
                    }
                    else if (description.Name.Equals(nameof(CarDiscountRule), StringComparison.OrdinalIgnoreCase))
                    {
                        _constraint = new CarDiscountRule(cars.FirstOrDefault(), description.Apply, description.Enabled);
                    }
                    else if (description.Name.Equals(nameof(CarYearDiscountRule), StringComparison.OrdinalIgnoreCase))
                    {
                        _constraint = new CarYearDiscountRule(cars.FirstOrDefault(), description.Apply, description.Enabled);
                    }
                }
            }
        }
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public Rule<ICar>? Rule => _constraint;

    /// <summary>
    /// 
    /// </summary>
    public Rule<IQueryable<ICar>>? Rules => _constraints;

    #endregion
}
