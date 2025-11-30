using BizCover.Cars.Models.Abstraction;

namespace BizCover.Cars.Data.Factory.Abstraction;

public partial interface ICarStoreRepository
{
    #region Functions

    /// <summary>
    /// Registers the vehicle synchronously into the backing storage.
    /// </summary>
    /// <param name="car">
    /// Nomiated vehicle
    /// </param>
    /// <returns>
    /// Vehicle identity
    /// </returns>
    int Add(ICar? car);

    /// <summary>
    /// Registers the vehicle asynchronously into the backing storage.
    /// </summary>
    /// <param name="car">
    /// Nomiated vehicle
    /// </param>
    /// <returns>
    /// Vehicle identity
    /// </returns>
    Task<int> AddAsync(ICar? car);

    /// <summary>
    /// Retrieves the vehicle details synchronously from the provided vehicle identity.
    /// </summary>
    /// <param name="id">
    /// Vehicle identity
    /// </param>
    /// <returns>
    /// Vehicle from the ensuing repository.
    /// </returns>
    ICar? Get(int id);

    /// <summary>
    /// Retrieves the vehicle details asynchronously from the provided vehicle identity.
    /// </summary>
    /// <param name="id">
    /// Vehicle identity
    /// </param>
    /// <returns>
    /// Vehicle from the ensuing repository.
    /// </returns>
    Task<ICar?> GetAsync(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    List<ICar> GetAll();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<List<ICar>> GetAllAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <param name="filename"></param>
    void Load(string? path, string? filename);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    bool Update(ICar? car);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> UpdateAsync(ICar? car);

    #endregion
}
