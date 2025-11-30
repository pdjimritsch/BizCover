using System.ComponentModel;

using BizCover.Cars.Models.Abstraction;
using BizCover.Cars.Repository.Abstraction;
using BizCover.Cars.RuleEngine;

namespace BizCover.Cars.Service;

using Abstraction;

[ImmutableObject(true)] public sealed partial class CarService : ICarService
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly ICarRepository? _carRepository;

    /// <summary>
    /// 
    /// </summary>
    private readonly List<RuleDescription> _descriptions;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="carRepository">
    /// Genric vehicle repository.
    /// </param>
    /// 
    public CarService(ICarRepository? carRepository) : base()
    {
        _carRepository = carRepository;

        var repository = new RuleRepository();

        var constraints = new CarRules(repository);

        _descriptions = constraints.RuleDescriptions;
    }

    #endregion

    #region IVehicleService Members

    /// <summary>
    /// Registers the generic vehicle brand into the data store.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand
    /// </param>
    /// <returns></returns>
    public bool Add(ICar? car)
    {
        if (car != null)
        {
            ApplyDiscount(ref car);

            return _carRepository?.Add(car) ?? false;
        }

        return false;
    }
    
    /// <summary>
    /// Registers the generic vehicle brand into the data store asynchronously.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand
    /// </param>
    /// <returns></returns>
    public async Task<bool> AddCarAsync(ICar? car)
    {
        if ((car != null) && (_carRepository != null))
        {
            ApplyDiscount(ref car);

            return await Task.FromResult(await _carRepository.AddAsync(car));
        }
        return false;
    }

    /// <summary>
    /// Gets the registered or unregistered generic vehicle brand from the data store.
    /// </summary>
    /// <returns></returns>
    public List<ICar> GetAllCars()
    {
        if (_carRepository != null) return _carRepository.GetAll();

        return [];
    }

    /// <summary>
    /// Gets the registered or unregistered generic vehicle brand 
    /// from the data store asynchronously.
    /// </summary>
    /// <returns></returns>
    public async Task<List<ICar>> GetAllCarsAsync()
    {
        if (_carRepository != null) return await _carRepository.GetAllAsync();

        return await Task.FromResult(new List<ICar>());
    }

    /// <summary>
    /// Upddate the geenric vehicle brand within the data store.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand.
    /// </param>
    public bool UpdateCar(ICar? car)
    {
        if ((_carRepository != null) && (car != null))
        {
            ApplyDiscount(ref car);

            return _carRepository.Update(car);
        }
        return false;
    }

    /// <summary>
    /// Upddate the geenric vehicle brand within the data store asynchronously.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand.
    /// </param>
    public async Task<bool> UpdateCarAsync(ICar? car)
    {
        if ((_carRepository != null) && (car != null) && (car.Id > 0))
        {
            // get the preserved vehicle from the repository

            var vehicle = await _carRepository.GetAsync(car.Id);

            if (vehicle != null && 
                ((vehicle.Year != car.Year) || (vehicle.Price != car.Price)))
            {
                RemoveDiscount(ref vehicle);

                vehicle.Year = car.Year;

                vehicle.Price = car.Price;

                ApplyDiscount(ref vehicle);
            }

           return await _carRepository.UpdateAsync(car);
        }

        return false;
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// Applies the specifc offered car retail discount 
    /// </summary>
    /// <param name="car"></param>
    private void ApplyDiscount(ref ICar car)
    {
        foreach (var description in _descriptions)
        {
            /*
             *  Apply the discount to the car / car collection and 
             *  discard the car discount rule automatically.
             *  
             *  The specific discount has been applied to the car retail price.
             */
            _ = new CarRule(description, [car]);
        }
    }

    private void RemoveDiscount(ref ICar car)
    {
        foreach (var description in _descriptions)
        {
            /*
             *  Remove the applied discount for the car / car collection and 
             *  discard the car discount rule automatically.
             *  
             *  The specific discount removal has been applied to the previous car retail price.
             */

            var aborted = description;

            aborted.Apply = false;

            _ = new CarRule(aborted, [car]);
        }

    }

    #endregion
}
