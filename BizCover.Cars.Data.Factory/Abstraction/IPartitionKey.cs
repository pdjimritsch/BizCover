namespace BizCover.Cars.Data.Factory.Abstraction;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public partial interface IPartitionKey<T> where T : class
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    T KeyEntry { get; set; }

    /// <summary>
    /// 
    /// </summary>
    ulong Position { get; set; }

    #endregion
}
