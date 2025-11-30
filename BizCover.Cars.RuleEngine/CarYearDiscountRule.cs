using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.RuleEngine;

/// <summary>
/// 
/// </summary>
public partial class CarYearDiscountRule : Rule<ICar>
{
    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    /// <param name="apply"></param>
    /// <param name="enabled"></param>
    public CarYearDiscountRule(ICar? car, bool apply = true, bool enabled = true) : base(car)
    {
        List<(dynamic Constraint, bool Enabled)> parameters = [];

        parameters.Add((Constraint: DiscountConstraint(car, apply? ApplyDiscount : RemoveDiscount), Enabled: enabled));

        Validate(parameters.ToArray());
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    private static dynamic DiscountConstraint(ICar? car, Action<ICar> action) => new
    {
        Condition = (car?.Year ?? int.MaxValue) < 2000, /* applicable only for cars older than year 2000 */
        Effect = action,
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    private void ApplyDiscount(ICar? car)
    {
        if (car != null && car.Price > 0)
        {
            var discount = (decimal)((double)car.Price * 0.1);
            car.Price -= discount;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    private void RemoveDiscount(ICar? car)
    {
        if (car != null && car.Price > 0)
        {
            car.Price = (decimal)(((double)car.Price * 10) / 9);
        }
    }

    #endregion
}