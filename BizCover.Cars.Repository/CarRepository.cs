using System.ComponentModel;

using Microsoft.EntityFrameworkCore;

using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Data.Factory.Abstraction;
using BizCover.Cars.Data.Factory.Repository;
using BizCover.Cars.Models.Abstraction;
using BizCover.Cars.Models;

namespace BizCover.Cars.Repository;

using Abstraction;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true)] public sealed partial class CarRepository : ICarRepository
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    readonly IServiceProvider? _provider;

    /// <summary>
    /// 
    /// </summary>
    readonly IContextConfiguration? _configuration;

    #region Repositories

    /// <summary>
    /// 
    /// </summary>
    readonly ICarStoreRepository _repository;

    /// <summary>
    /// Completmentary storage within SQL Server, MySql or MongoDB
    /// </summary>
    readonly IDataManager? _dataManager;

    #endregion

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataManager"></param>
    public CarRepository(IDataManager? dataManager, 
        IContextConfiguration? configuration, IServiceProvider? provider = null) : base()
    {
        _configuration = configuration;

        _provider = provider;

        _dataManager = dataManager;

        _repository = new CarStoreRepository();

        var directory = AppDomain.CurrentDomain.BaseDirectory;

        var location = Path.Combine(directory, "Content");

        _repository.Load(location, "BizCover.Repository.Cars.cars.json");
    }

    /// <summary>
    /// 
    /// </summary>
    public CarRepository(
        IContextConfiguration? configuration, IServiceProvider? provider = null) : base()
    {
        _configuration = configuration;

        _provider = provider;

        _dataManager = null;

        _repository = new CarStoreRepository();

        var directory = AppDomain.CurrentDomain.BaseDirectory;

        var location = Path.Combine(directory, "Content");

        _repository.Load(location, "BizCover.Repository.Cars.cars.json");
    }

    #endregion

    #region ICarRepository Members

    /// <summary>
    /// Registers the generic vehicle brand synchronously into the data store.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand.
    /// </param>
    /// <returns>
    /// True if the vehicle has been registered within the data store,
    /// otherwise Falae.
    /// </returns>
    public bool Add(ICar? car)
    {
        // complementary data storage (primary storage)

        var response = (_repository.Add(car) > 0);

        if (_dataManager != null /* complementary data storage */)
        {
            var entity = _dataManager.Add(Car.TransForm(car));

            response = response && (entity != null) && (entity.State == EntityState.Added);
        }

        return response;
    }

    /// <summary>
    /// Registers the generic vehicle brand asynchronously into the data store.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand.
    /// </param>
    /// <returns>
    /// True if the vehicle has been registered within the data store,
    /// otherwise Falae.
    /// </returns>
    public async Task<bool> AddAsync(ICar? car)
    {
        // complementary data storage (primaery storage)

        var identity = await _repository.AddAsync(car);

        var response = (identity > 0);

        if (_dataManager != null /* complementary data storage */)
        {
            var entity = await _dataManager.AddAsync(Car.TransForm(car));

            response = response && (entity != null) && (entity.State == EntityState.Added);
        }

        return response;
    }

    /// <summary>
    /// Retrieves the vehicle details from the provided vehicle identity.
    /// </summary>
    /// <param name="id">
    /// Vehicle identity
    /// </param>
    /// <returns>
    /// Vehicle from the ensuing repository.
    /// </returns>
    public ICar? Get(int id)
    {
        // complementary data storage (primaery storage)

        var vehicle = _repository.Get(id);

        if (_dataManager != null /* complementary data storage */)
        {
            var car = _dataManager.FirstOrDefault<Car>(v => v.Id == id);

            if (car != null && vehicle == null)
            {
                vehicle = car;
            }
            else if (car != null && vehicle != null)
            {
                if (!car.Equals(vehicle))
                {
                    _dataManager.Add(Car.TransForm(vehicle));
                }
            }
        }

        return vehicle;
    }

    /// <summary>
    /// Retrieves the vehicle details from the provided vehicle identity.
    /// </summary>
    /// <param name="id">
    /// Vehicle identity
    /// </param>
    /// <returns>
    /// Vehicle from the ensuing repository.
    /// </returns>
    public async Task<ICar?> GetAsync(int id)
    {
        // complementary data storage (primaery storage)

        var vehicle = await _repository.GetAsync(id);

        if (_dataManager != null /* complementary data storage */)
        {
            var car = await _dataManager.FirstOrDefaultAsync<Car>(v => v.Id == id);

            if (car != null && vehicle == null)
            {
                vehicle = car;
            }
            else if (car != null && vehicle != null)
            {
                if (!car.Equals(vehicle))
                {
                    await _dataManager.AddAsync(Car.TransForm(vehicle));
                }
            }
        }

        return vehicle;
    }


    /// <summary>
    /// Gets the registered or unregistered generic vehicle brands 
    /// from the data store in an sychronous manner.
    /// </summary>
    /// <returns>
    /// Rregistered or unregistered generic vehicle brands.
    /// </returns>
    public List<ICar> GetAll()
    {
        // complementary data storage (primaery storage)

        var vehicles = _repository.GetAll();

        if (_dataManager != null /* complementary data storage */)
        {
            if (vehicles == null)
            {
                var cars = _dataManager.All<Car, Car>();

                vehicles = new List<ICar>(cars);
            }
        }

        return vehicles;
    }

    /// <summary>
    /// Gets the registered or unregistered generic vehicle brands 
    /// from the data store in an asychronous manner.
    /// </summary>
    /// <returns>
    /// Rregistered or unregistered generic vehicle brands.
    /// </returns>
    public async Task<List<ICar>> GetAllAsync()
    {
        // complementary data storage (primary storage)

        var vehicles = await _repository.GetAllAsync();

        if (_dataManager != null /* complementary data storage */)
        {
            if (vehicles == null)
            {
                var cars = await _dataManager.AllAsync<Car, Car>();

                vehicles = new List<ICar>(cars);
            }
        }

        return await Task.FromResult(vehicles);
    }

    /// <summary>
    /// Upddate the geenric vehicle brand within the data store.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand
    /// </param>
    public bool Update(ICar? car)
    {
        // complementary data storage (primaery storage)

        var response = _repository.Update(car);

        if (_dataManager != null /* complementary data storage */)
        {
            var entity = _dataManager.Update(Car.TransForm(car));

            response = response && (entity != null) && (entity.State == EntityState.Modified);
        }

        return response;
    }

    /// <summary>
    /// Upddate the geenric vehicle brand within the data store asynchronously.
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(ICar? car)
    {
        // complementary data storage (primaery storage)

        var response = await _repository.UpdateAsync(car);

        if (_dataManager != null /* complementary data storage */)
        {
            var entity = await _dataManager.UpdateAsync(Car.TransForm(car));

            response = response && (entity != null) && (entity.State == EntityState.Modified);
        }

        return response;
    }

    #endregion
}
