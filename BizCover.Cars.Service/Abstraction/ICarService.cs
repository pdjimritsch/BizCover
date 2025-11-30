using BizCover.Cars.Models;
using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.Service.Abstraction;

/// <summary>
/// 
/// </summary>
public partial interface ICarService
{
    #region Members

    /// <summary>
    /// Registers the generic vehicle brand into the data store.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand
    /// </param>
    /// <returns>
    /// </returns>
    bool Add(ICar? car);

    /// <summary>
    /// Registers the generic vehicle brand into the data store asynchronously.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand
    /// </param>
    /// <returns></returns>
    Task<bool> AddCarAsync(ICar? car);

    /// <summary>
    /// Gets the registered or unregistered generic vehicle brand from the data store.
    /// </summary>
    /// <returns></returns>
    List<ICar> GetAllCars();

    /// <summary>
    /// Gets the registered or unregistered generic vehicle brand 
    /// from the data store asynchronously.
    /// </summary>
    /// <returns></returns>
    Task<List<ICar>> GetAllCarsAsync();

    /// <summary>
    /// Upddate the geenric vehicle brand within the data store.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand
    /// </param>
    bool UpdateCar(ICar? car);

    /// <summary>
    /// Upddate the geenric vehicle brand within the data store asynchronously.
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    Task<bool> UpdateCarAsync(ICar? car);

    #endregion
}
