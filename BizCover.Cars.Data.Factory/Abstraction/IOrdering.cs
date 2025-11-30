namespace BizCover.Cars.Data.Factory.Abstraction;

using Enumerations;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="OrderBy"></typeparam>
public interface IOrdering<T> where T : class
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    SortDirection Direction { get; set; }

    /// <summary>
    /// 
    /// </summary>
    object Entry { get; set; }

    /// <summary>
    /// 
    /// </summary>
    Func<T, object> SortBy { get; set; }

    #endregion
}
