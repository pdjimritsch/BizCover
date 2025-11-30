using BizCover.Cars.Configuration.Enumerations;
using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Configuration.Abstraction;

public partial interface IContextConfiguration
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    string? AllowedHosts { get; }

    /// <summary>
    /// Authorization for web API based access
    /// </summary>
    string? ApiKey { get; }

    /// <summary>
    /// 
    /// </summary>
    string? ApiSubscriber { get; }

    /// <summary>
    /// 
    /// </summary>
    int? AppConnectTimeout { get; }

    /// <summary>
    /// 
    /// </summary>
    string? ApplicationName { get; }

    /// <summary>
    /// 
    /// </summary>
    string? AuthenticationMode { get; }

    /// <summary>
    /// 
    /// </summary>
    IConfiguration? Configuration { get; }
    
    /// <summary>
    /// 
    /// </summary>
    ConnectionType ConnectionType { get; }    

    /// <summary>
    /// 
    /// </summary>
    int? CookieExpiration { get; }

    /// <summary>
    /// 
    /// </summary>
    ActiveEnvironment Environment { get; set; }

    /// <summary>
    /// 
    /// </summary>
    int? RequestTimeout { get; }

    /// <summary>
    /// 
    /// </summary>
    IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 
    /// </summary>
    int? SessionTimeout { get; }

    /// <summary>
    /// 
    /// </summary>
    string? SwaggerVersion { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? UseAuthentication { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? UseAzure { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? UseCors { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? UseSwagger { get; }

    /// <summary>
    /// 
    /// </summary>
    IWebApiRoute? WebApiRoute { get; }

    /// <summary>
    /// 
    /// </summary>
    IWebToken? WebToken { get; }

    #endregion
}
