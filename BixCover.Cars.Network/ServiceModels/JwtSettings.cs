using System.ComponentModel; 

namespace BizCover.Cars.Network.ServiceModels;

/// <summary>
/// 
/// </summary>
[ImmutableObject(false)]
public sealed partial class JwtSettings
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public string? IssuerSigningKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool RequireExpirationTime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool ValidateAudience { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ValidAudience { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool ValidateIssuerSigningKey { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool ValidateIssuer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool ValidateLifetime { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ValidIssuer { get; set; }

    #endregion
}
