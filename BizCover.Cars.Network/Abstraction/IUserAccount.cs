namespace BizCover.Cars.Network.Abstraction;

/// <summary>
/// 
/// </summary>
public partial interface IUserAccount
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    DateTimeOffset? Expiry { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string? Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    string? Token { get; set; }

    /// <summary>
    /// 
    /// </summary>
    TimeSpan? Validity { get; set; }

    #endregion
}
