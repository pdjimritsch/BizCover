using BizCover.Cars.Models;
using BizCover.Cars.Models.Abstraction;
using BizCover.Cars.RuleEngine;
using NUnit.Framework;
using System.ComponentModel;

using NAssert = NUnit.Framework.Assert;

namespace BizCover.Cars.RulesEngine.Test;

[ImmutableObject(true), TestFixture, Parallelizable(ParallelScope.All)]
public class CarDiscountTest
{
    #region Tests

    /// <summary>
    /// Test the car discount rule only.
    /// </summary>
    [Test]
    public void ApplyCarDiscountRuleForApproval()
    {
        var price = 150000m;

        Car discounted = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = price,
            Year = 2002,
        };

        // apply 5 percent discount to the vehicle original price
        _ = new CarDiscountRule(discounted);

        // apply 5% discount
        var discountedPrice = (decimal)((double)price * .95);

        NAssert.That(discounted.Price == discountedPrice, Is.True);
    }

    /// <summary>
    /// Test the car discount rule only.
    /// </summary>
    [Test]
    public void UnapplyCarDiscountRuleForApproval()
    {
        var price = 150000m;

        Car discounted = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = price,
            Year = 2002,
        };

        // apply 5 percent discount to the vehicle original price
        _ = new CarDiscountRule(discounted, false);

        // apply 5% discount
        var discountedPrice = (decimal)((double)price * .95);

        NAssert.That(discounted.Price == discountedPrice, Is.False);

        NAssert.That(discounted.Price == price, Is.True);
    }

    /// <summary>
    /// Test the car discount rule only.
    /// </summary>
    [Test]
    public void ApplyCarYearDiscountRuleForApproval()
    {
        var price = 150000m;

        Car discounted = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = price,
            Year = 1998,
        };

        /*
         * Apply 10 percent discount to the vehicle original price
         * for cars that are older that year 2000.
         */
        _ = new CarYearDiscountRule(discounted);

        // apply 10% discount
        var discountedPrice = (decimal)((double)price * .90);

        NAssert.That(discounted.Price == discountedPrice, Is.True);
    }

    /// <summary>
    /// Test the car discount rule only.
    /// </summary>
    [Test]
    public void ApplyYoungerCarYearDiscountRuleForApproval()
    {
        var price = 150000m;

        Car discounted = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = price,
            Year = 2002,
        };

        /*
         * Apply 10 percent discount to the vehicle original price
         * for cars that are older that year 2000.
         */
        _ = new CarYearDiscountRule(discounted);

        // apply 10% discount
        var discountedPrice = (decimal)((double)price * .90);

        NAssert.That(discounted.Price == discountedPrice, Is.False);
    }

    /// <summary>
    /// Test the car discount rule only.
    /// </summary>
    [Test]
    public void UnapplyCarYearDiscountRuleForApproval()
    {
        var price = 150000m;

        Car discounted = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = price,
            Year = 1998,
        };

        // apply 5 percent discount to the vehicle original price
        _ = new CarDiscountRule(discounted, false);

        // apply 10% discount
        var discountedPrice = (decimal)((double)price * .95);

        NAssert.That(discounted.Price == discountedPrice, Is.False);
    }

    /// <summary>
    /// Test the car volume discount rule only.
    /// </summary>
    [Test]
    public void ApplyCarVolumeDiscountRuleForApproval()
    {
        Car australian = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = 150000m,
            Year = 2002,
        };

        Car nz = new()
        {
            Colour = "Red",
            CountryManufactured = "New Zealand",
            Make = "Toyota",
            Model = "Sedan",
            Price = 200000m,
            Year = 2002,
        };

        Car usa = new()
        {
            Colour = "Red",
            CountryManufactured = "United States of America",
            Make = "Toyota",
            Model = "Sedan",
            Price = 250000m,
            Year = 2002,
        };

        var cars = new List<Car> { australian, nz, usa };

        /*
         * Apply 3 percent discount to the vehicle original price
         * for cars that are processed.
         */
        _ = new CarVolumeDiscountRule(cars.AsQueryable());

        // apply 3% discount

        int compared = 0;

        foreach (var car in cars)
        {
            bool validated = false;

            if (car?.CountryManufactured?.Equals("Australia", StringComparison.Ordinal) ?? false)
            {
                compared++;

                decimal discount = 150000m - (decimal)(150000 * .03);

                validated = (car?.Price ?? 0m) == discount;
            }
            else if (car?.CountryManufactured?.Equals("New Zealand", StringComparison.Ordinal) ?? false)
            {
                compared++;

                decimal discount = 200000m - (decimal)(200000 * .03);

                validated = (car?.Price ?? 0m) == discount;
            }
            else if (car?.CountryManufactured?.Equals("United States of America", StringComparison.Ordinal) ?? false)
            {
                compared++;

                decimal discount = 250000m - (decimal)(250000 * .03);

                validated = (car?.Price ?? 0m) == discount;
            }

            NAssert.That(validated, Is.True);
        }

        NAssert.That(compared == 3, Is.True);
    }


    /// <summary>
    /// Test the car volume discount rule only.
    /// </summary>
    [Test]
    public void ApplySingleCarVolumeDiscountRuleForApproval()
    {
        Car australian = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = 150000m,
            Year = 2002,
        };

        var cars = new List<Car> { australian };

        /*
         * Apply 3 percent discount to the vehicle original price
         * for cars that are processed.
         */
        _ = new CarVolumeDiscountRule(cars.AsQueryable());

        // apply 3% discount

        int compared = 0;

        foreach (var car in cars)
        {
            bool validated = false;

            if (car?.CountryManufactured?.Equals("Australia", StringComparison.Ordinal) ?? false)
            {
                compared++;

                decimal discount = 150000m;

                validated = (car?.Price ?? 0m) == discount;
            }

            NAssert.That(validated, Is.True);
        }

        NAssert.That(compared == 1, Is.True);
    }

    /// <summary>
    /// Test the car volume discount rule only.
    /// </summary>
    [Test]
    public void UnapplyCarVolumeDiscountRuleForApproval()
    {
        Car australian = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = 150000m,
            Year = 2002,
        };

        Car nz = new()
        {
            Colour = "Red",
            CountryManufactured = "New Zealand",
            Make = "Toyota",
            Model = "Sedan",
            Price = 200000m,
            Year = 2002,
        };

        Car usa = new()
        {
            Colour = "Red",
            CountryManufactured = "United States of America",
            Make = "Toyota",
            Model = "Sedan",
            Price = 250000m,
            Year = 2002,
        };

        var cars = new List<Car> { australian, nz, usa };

        /*
         * Apply 3 percent discount to the vehicle original price
         * for cars that are processed.
         */
        _ = new CarVolumeDiscountRule(cars.AsQueryable(), false);

        // apply 3% discount

        int compared = 0;

        foreach (var car in cars)
        {
            bool validated = false;

            if (car?.CountryManufactured?.Equals("Australia", StringComparison.Ordinal) ?? false)
            {
                compared++;

                decimal discount = 150000m;

                validated = (car?.Price ?? 0m) == discount;
            }
            else if (car?.CountryManufactured?.Equals("New Zealand", StringComparison.Ordinal) ?? false)
            {
                compared++;

                decimal discount = 200000m;

                validated = (car?.Price ?? 0m) == discount;
            }
            else if (car?.CountryManufactured?.Equals("United States of America", StringComparison.Ordinal) ?? false)
            {
                compared++;

                decimal discount = 250000m;

                validated = (car?.Price ?? 0m) == discount;
            }


            NAssert.That(validated, Is.True);
        }

        NAssert.That(compared == 3, Is.True);
    }

    /// <summary>
    /// 
    /// </summary>
    [Test] public void ApplyAllRulesFor3CarsForApproval()
    {
        // all vehicles are aged (prior year 2000)

        Car australian = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = 150000m,
            Year = 1998,
        };

        Car nz = new()
        {
            Colour = "Red",
            CountryManufactured = "New Zealand",
            Make = "Toyota",
            Model = "Sedan",
            Price = 200000m,
            Year = 1997,
        };

        Car usa = new()
        {
            Colour = "Red",
            CountryManufactured = "United States of America",
            Make = "Toyota",
            Model = "Sedan",
            Price = 250000m,
            Year = 1996,
        };

        // apply the business offered discounts

        var cars = new List<Car> { australian, nz, usa };

        var constraints = new List<Rule<ICar>>
        {
            new CarDiscountRule(australian),
            new CarDiscountRule(nz),
            new CarDiscountRule(usa),
            new CarYearDiscountRule(australian),
            new CarYearDiscountRule(nz),
            new CarYearDiscountRule(usa),
        };

        _ = new CarVolumeDiscountRule(cars.AsQueryable());

        int compared = 0;

        foreach (var car in cars)
        {
            var approved = false;

            decimal price = 0m;

            if (car?.CountryManufactured?.Equals("Australia", StringComparison.Ordinal) ?? false)
            {
                price = 150000m;

                ++compared;
            }
            else if (car?.CountryManufactured?.Equals("New Zealand", StringComparison.Ordinal) ?? false)
            {
                price = 200000m;

                ++compared;
            }
            else if (car?.CountryManufactured?.Equals("United States of America", StringComparison.Ordinal) ?? false)
            {
                price = 250000m;

                ++compared;
            }

            // apply car 5% discount rule
            var discount = (decimal)((double)price * .05);
            price -= discount;

            // apply older (aged) car 10% discount rule
            discount = (decimal)((double)price * .1);
            price -= discount;

            // apply 3% volume trading discount
            discount = (decimal)((double)price * .03);
            price -= discount;

            approved = (car?.Price ?? 0) == price;

            NAssert.That(approved, Is.True);
        }

        NAssert.That(compared == 3, Is.True);
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public void UnapplyAllRulesFor3CarsForApproval()
    {
        // all vehicles are aged (prior year 2000)

        Car australian = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = 150000m,
            Year = 1998,
        };

        Car nz = new()
        {
            Colour = "Red",
            CountryManufactured = "New Zealand",
            Make = "Toyota",
            Model = "Sedan",
            Price = 200000m,
            Year = 1997,
        };

        Car usa = new()
        {
            Colour = "Red",
            CountryManufactured = "United States of America",
            Make = "Toyota",
            Model = "Sedan",
            Price = 250000m,
            Year = 1996,
        };

        // apply the business offered discounts

        var cars = new List<Car> { australian, nz, usa };

        var constraints = new List<Rule<ICar>>
        {
            new CarDiscountRule(australian, false),
            new CarDiscountRule(nz, false),
            new CarDiscountRule(usa, false),
            new CarYearDiscountRule(australian, false),
            new CarYearDiscountRule(nz, false),
            new CarYearDiscountRule(usa, false),
        };

        _ = new CarVolumeDiscountRule(cars.AsQueryable(), false);

        int compared = 0;

        foreach (var car in cars)
        {
            var approved = false;

            decimal price = 0m;

            if (car?.CountryManufactured?.Equals("Australia", StringComparison.Ordinal) ?? false)
            {
                price = 150000m;

                ++compared;
            }
            else if (car?.CountryManufactured?.Equals("New Zealand", StringComparison.Ordinal) ?? false)
            {
                price = 200000m;

                ++compared;
            }
            else if (car?.CountryManufactured?.Equals("United States of America", StringComparison.Ordinal) ?? false)
            {
                price = 250000m;

                ++compared;
            }

            approved = (car?.Price ?? 0) == price;

            NAssert.That(approved, Is.True);
        }

        NAssert.That(compared == 3, Is.True);
    }

    #endregion
}
