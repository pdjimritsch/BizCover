using System.ComponentModel;

namespace Vault.Authentication.ServiceModels;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true)]
public sealed partial class UserToken
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TimeSpan? Expires { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? RefreshToken { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? UserName { get; set; }

    #endregion
}
