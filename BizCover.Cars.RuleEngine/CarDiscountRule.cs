using BizCover.Cars.Models.Abstraction;
using Newtonsoft.Json.Bson;
using System.Data;

namespace BizCover.Cars.RuleEngine;

public partial class CarDiscountRule : Rule<ICar>
{
    #region Constructors

    /// <summary>
    /// Applies or reverts the applied vehicle discount for the vehicle
    /// </summary>
    /// <param name="car"></param>
    public CarDiscountRule(ICar? car, bool apply = true, bool enabled = true) : base(car)
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
        Condition = (car?.Price ?? 0m) > 100000m,
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
            var discount = (decimal)((double)car.Price * 0.05);
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
            var original = (decimal)((double)car.Price * 100) / 95;
            car.Price = original;
        }
    }

    #endregion
}
