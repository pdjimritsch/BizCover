using BizCover.Cars.Data.Factory.Repository;
using BizCover.Cars.Models;
using NUnit.Framework;
using System.ComponentModel;
using NAssert = NUnit.Framework.Assert;

namespace BizCover.Cars.Data.Factory.Test;

[Serializable, ImmutableObject(true), TestFixture]
public sealed partial class CarStoreRepositoryTest
{
    #region Tests

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public void AddCarToRepositoryForApproval()
    {
        var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content");

        CarStoreRepository repository = new();

        repository.Load(directory, "BizCover.Repository.Cars.cars.json");

        var dictionary = repository.GetAll();

        NAssert.That(dictionary, Is.Not.Null);

        var count = dictionary.Count;

        NAssert.That(count, Is.GreaterThan(0));

        Car discounted = new()
        {
            Colour = "Red",
            CountryManufactured = "Australia",
            Make = "Toyota",
            Model = "Sedan",
            Price = 150000m,
            Year = 2002,
        };

        var identity = repository.Add(discounted);

        NAssert.That(identity > 0, Is.True);

        var preserved = Car.TransForm(repository.Get(identity));

        NAssert.That(preserved, Is.Not.Null);

        dictionary = repository.GetAll();

        NAssert.That(dictionary.Count, Is.GreaterThan(count));

        if (preserved != null)
        {
            NAssert.That(preserved.Id == identity);

            NAssert.That(preserved.Colour?.Equals(discounted.Colour, StringComparison.Ordinal) ?? false, Is.True);

            NAssert.That(preserved.CountryManufactured?.Equals(discounted.CountryManufactured) ?? false, Is.True);

            NAssert.That(preserved.Make?.Equals(discounted.Make, StringComparison.Ordinal) ?? false, Is.True);

            NAssert.That(preserved.Model?.Equals(discounted.Model, StringComparison.Ordinal) ?? false, Is.True);

            NAssert.That(preserved.Price == discounted.Price);

            NAssert.That(preserved.Year == discounted.Year, Is.True);
        }
        else
        {
            NAssert.Fail();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public void GetCarsFromRepositoryForApproval()
    {
        var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content");

        CarStoreRepository repository = new();

        repository.Load(directory, "BizCover.Repository.Cars.cars.json");

        var dictionary = repository.GetAll();

        NAssert.That(dictionary, Is.Not.Null);

        NAssert.That(dictionary.Count, Is.GreaterThan(0));
    }

    /// <summary>
    /// 
    /// </summary>
    [Test]
    public void UpdateCarFromRepositoryForApproval()
    {
        var directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content");

        CarStoreRepository repository = new();

        repository.Load(directory, "BizCover.Repository.Cars.cars.json");

        var dictionary = repository.GetAll();

        NAssert.That(dictionary, Is.Not.Null);

        var count = dictionary.Count;

        NAssert.That(count, Is.GreaterThan(0));

        var entry = dictionary[count - 1];

        NAssert.That(entry, Is.Not.Null);

        entry.Price = 800000m;

        var posted = repository.Update(entry);

        NAssert.That(posted, Is.True);

        var updated = dictionary[count - 1];

        NAssert.That(entry.Price == updated.Price, Is.True);
    }

    #endregion
}
