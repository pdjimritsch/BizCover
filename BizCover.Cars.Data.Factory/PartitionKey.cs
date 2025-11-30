namespace BizCover.Cars.Data.Factory;

using Abstraction;

/// <summary>
/// 
/// </summary>
public partial class PartitionKey<T> : IPartitionKey<T> where T : class
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public required T KeyEntry { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ulong Position { get; set; } = default;

    #endregion
}
