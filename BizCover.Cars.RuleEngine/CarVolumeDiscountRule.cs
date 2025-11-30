using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.RuleEngine;

/// <summary>
/// 
/// </summary>
public partial class CarVolumeDiscountRule : Rule<IQueryable<ICar>>
{
    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cars"></param>
    /// <param name="apply"></param>
    /// <param name="enabled"></param>
    public CarVolumeDiscountRule(IQueryable<ICar>? cars, bool apply = true, bool enabled = true) : base(cars)
    {
        List<(dynamic Constraint, bool Enabled)> parameters = [];

        parameters.Add((Constraint: DiscountConstraint(cars, apply ? ApplyDiscount : RemoveDiscount), Enabled: enabled));

        Validate(parameters.ToArray());
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    private static dynamic DiscountConstraint(IQueryable<ICar>? cars, Action<IQueryable<ICar>> action) => new
    {
        Condition = (cars?.ToArray().Length ?? 0) > 2,
        Effect = action,
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="car"></param>
    private void ApplyDiscount(IQueryable<ICar>? cars)
    {
        /* applicable only for more than 2 cars */
        foreach (ICar? car in cars ?? Enumerable.Empty<ICar>())
        {
            if (car != null)
            {
                // Apply 3% discount
                var discount = (decimal)((double)car.Price * 0.03);
                car.Price -= discount;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cars"></param>
    private void RemoveDiscount(IQueryable<ICar>? cars)
    {
        foreach (ICar? car in cars ?? Enumerable.Empty<ICar>())
        {
            if (car != null)
            {
                var price = (decimal)(((double)car.Price * 100) / 97);

                car.Price = price;
            }
        }
    }

    #endregion

}
