namespace BizCover.Cars.Data.Factory.Abstraction;

using Enumerations;

/// <summary>
/// 
/// </summary>
public interface IDataFunctions : IAsyncDisposable, IDisposable
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selector"></param>
    /// <param name="dt"></param>
    /// <returns></returns>
    int? DatePart(DatePartOffset selector, DateTime dt);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <param name="selector"></param>
    /// <param name="dt"></param>
    /// <returns></returns>merations
    int? DateOffsetPart(DatePartOffset selector, DateTimeOffset dt);

    #endregion
}
