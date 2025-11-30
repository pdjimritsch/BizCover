using BizCover.Cars.Models;
using NUnit.Framework;
using System.ComponentModel;
using NAssert = NUnit.Framework.Assert;

namespace BizCover.Cars.RuleEngine.Test;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true), TestFixture, Parallelizable(ParallelScope.All)]
public partial class RuleRepositoryTest 
{
    #region Tests

    /// <summary>
    ///  Loads the business rules from the JSON file.
    /// </summary>
    [Test] public void LoadBusinessRulesForApproval()
    {
        RuleRepository repository = new();

        repository.Load("Content", "Car.Rules.json");

        NAssert.That(repository.Exception, Is.Null);

        NAssert.That(repository.List.Count, Is.GreaterThan(0));
    }

    /// <summary>
    /// 
    /// </summary>
    [Test] public void ApplyDiscountRulesForCarWithApproval()
    {
        decimal price = 150000m;

        int year = 1998;

        Car vehicle = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = price,
            Year = year,
        };

        // get the discount rules

        var repository = new RuleRepository();

        var constraints = new CarRules(repository);

        var descriptions = constraints.RuleDescriptions;

        NAssert.That(descriptions, Is.Not.Null);

        NAssert.That(descriptions, Is.Not.Empty);

        NAssert.That(descriptions.Count, Is.GreaterThan(0));

        NAssert.That(descriptions.All(d => d.Enabled), Is.True);

        // apply the discount rules to the vehicle

        foreach (var description in descriptions)
        {
            /*
             *  Apply the discount to the car / car collection and 
             *  discard the car discount rule automatically.
             *  
             *  The specific discount has been applied to the car retail price.
             */
            _ = new CarRule(description, [vehicle]);
        }

        NAssert.That(vehicle.Year == year, Is.True);

        NAssert.That(vehicle.Price < price, Is.True);
    }


    /// <summary>
    /// 
    /// </summary>
    [Test]
    public void ApplyNoDiscountRulesForCarWithApproval()
    {
        decimal price = 150000m;

        int year = 1998;

        Car vehicle = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = price,
            Year = year,
        };

        // get the discount rules

        var repository = new RuleRepository();

        var constraints = new CarRules(repository);

        var descriptions = constraints.RuleDescriptions;

        NAssert.That(descriptions, Is.Not.Null);

        NAssert.That(descriptions, Is.Not.Empty);

        NAssert.That(descriptions.Count, Is.GreaterThan(0));

        // remove all discounts

        descriptions.ToList().ForEach(d => d.Enabled = false);

        // apply the discount rules to the vehicle

        foreach (var description in descriptions)
        {
            /*
             *  Apply the discount to the car / car collection and 
             *  discard the car discount rule automatically.
             *  
             *  The specific discount has been applied to the car retail price.
             */
            _ = new CarRule(description, [vehicle]);
        }

        NAssert.That(vehicle.Year == year, Is.True);

        NAssert.That(vehicle.Price == price, Is.True);
    }

    #endregion
}