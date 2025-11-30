using BizCover.Cars.Models.Abstraction; 

namespace BizCover.Cars.Repository.Abstraction;

/// <summary>
/// Vehicle representation for the back-end data storage
/// </summary>
public partial interface ICarRepository
{
    #region Members

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
    bool Add(ICar? car);

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
    Task<bool> AddAsync(ICar? car);

    /// <summary>
    /// Retrieves the vehicle details from the provided vehicle identity.
    /// </summary>
    /// <param name="id">
    /// Vehicle identity
    /// </param>
    /// <returns>
    /// Vehicle from the ensuing repository.
    /// </returns>
    ICar? Get(int id);

    /// <summary>
    /// Retrieves the vehicle details from the provided vehicle identity.
    /// </summary>
    /// <param name="id">
    /// Vehicle identity
    /// </param>
    /// <returns>
    /// Vehicle from the ensuing repository.
    /// </returns>
    Task<ICar?> GetAsync(int id);

    /// <summary>
    /// Gets the registered or unregistered generic vehicle brands 
    /// from the data store in an sychronous manner.
    /// </summary>
    /// <returns>
    /// Rregistered or unregistered generic vehicle brands.
    /// </returns>
    List<ICar> GetAll();

    /// <summary>
    /// <summary>
    /// Gets the registered or unregistered generic vehicle brands 
    /// from the data store in an asychronous manner.
    /// </summary>
    /// <returns>
    /// Rregistered or unregistered generic vehicle brands.
    /// </returns>
    Task<List<ICar>> GetAllAsync();

    /// <summary>
    /// Upddate the geenric vehicle brand within the data store.
    /// </summary>
    /// <param name="car">
    /// Generic vehicle brand
    /// </param>
    bool Update(ICar? car);

    /// <summary>
    /// Upddate the geenric vehicle brand within the data store asynchronously.
    /// </summary>
    /// <param name="car"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(ICar? car);

    #endregion
}
