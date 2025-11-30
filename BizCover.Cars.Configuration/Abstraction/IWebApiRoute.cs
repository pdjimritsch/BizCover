using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Configuration.Abstraction;

public partial interface IWebApiRoute
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    string? ApiRoute { get; }

    /// <summary>
    /// 
    /// </summary>
    IConfiguration? Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? IsRemote { get; }

    /// <summary>
    /// 
    /// </summary>
    string? RemoteUrl { get; }

    /// <summary>
    /// 
    /// </summary>
    IServiceProvider? ServiceProvider { get; }

    #endregion
}
