using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Configuration.Abstraction;

/// <summary>
/// 
/// </summary>
public partial interface IWebToken
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    IConfiguration? Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    string? IssuerSigningKey { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? RequireExpirationTime { get; }

    /// <summary>
    /// 
    /// </summary>
    IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 
    /// </summary>
    string? ValidIssuer { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? ValidateAudience { get; }

    /// <summary>
    /// 
    /// </summary>
    string? ValidAudience { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? ValidateIssuer { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? ValidateIssuerSigningKey { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? ValidateLifetime { get; }

    #endregion
}