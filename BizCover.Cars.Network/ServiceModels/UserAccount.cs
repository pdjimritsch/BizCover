using System.ComponentModel;

namespace BizCover.Cars.Network.ServiceModels;
using Abstraction;

/// <summary>
/// 
/// </summary>
[ImmutableObject(false)]
public sealed partial class UserAccount : IUserAccount
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset? Expiry { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TimeSpan? Validity { get; set; }

    #endregion
}
