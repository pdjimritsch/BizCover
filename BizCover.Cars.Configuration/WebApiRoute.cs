using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Configuration;
using Abstraction;

/// <summary>
/// 
/// </summary>
public partial class WebApiRoute : IWebApiRoute
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public const string SectionName = @"WebApi";

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="serviceProvider"></param>
    public WebApiRoute(IConfiguration? configuration, IServiceProvider? serviceProvider = null) : base()
    {
        ApiRoute = configuration?.GetValue<string?>($"{SectionName}:{nameof(ApiRoute)}") ?? "/";

        Configuration = configuration;

        IsRemote = configuration?.GetValue<bool?>($"{SectionName}:{nameof(IsRemote)}") ?? false;

        RemoteUrl = configuration?.GetValue<string?>($"{SectionName}:{nameof(RemoteUrl)}") ?? string.Empty;

        ServiceProvider = serviceProvider;
    }

    #endregion

    #region IWebApiRoute Members

    /// <summary>
    /// 
    /// </summary>
    public string? ApiRoute { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public IConfiguration? Configuration { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? IsRemote { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string? RemoteUrl { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider? ServiceProvider { get; private set; }

    #endregion
}
