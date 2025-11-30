namespace BizCover.Cars.Configuration;

using Abstraction;
using BizCover.Cars.Configuration.Enumerations;
using Microsoft.Extensions.Configuration;

public partial class ContextConfiguration : IContextConfiguration
{
    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="provider"></param>
    public ContextConfiguration(IConfiguration? configuration, IServiceProvider? provider = null) : base()
    {
        AllowedHosts = configuration?.GetValue<string?>(nameof(AllowedHosts));

        ApiKey = configuration?.GetValue<string>($"Security:{nameof(ApiKey)}");

        ApiSubscriber = configuration?.GetValue<string?>($"Subscribers:{nameof(ApiSubscriber)}");

        AppConnectTimeout = configuration?.GetValue<int?>("application:AppConnectTimeout");

        ApplicationName = configuration?.GetValue<string?>("application:name");

        AuthenticationMode = configuration?.GetValue<string?>(nameof(AuthenticationMode));

        Configuration = configuration;

        CookieExpiration = configuration?.GetValue<int?>(@"Cookie:Expiration");

        var value = configuration?.GetValue<string?>(nameof(ConnectionType));

        if (string.IsNullOrEmpty(value))
        {
            ConnectionType = ConnectionType.Memory;
        }
        else if (Enum.TryParse(value.Trim(), out ConnectionType response))
        {
            ConnectionType = response;
        }
        else
        {
            ConnectionType = ConnectionType.Memory;
        }

        RequestTimeout = configuration?.GetValue<int?>(nameof(RequestTimeout));

        ServiceProvider = provider;

        SessionTimeout = configuration?.GetValue<int?>(@"SessionTimeout");

        SwaggerVersion = configuration?.GetValue<string?>("Swagger:Version");

        UseAuthentication = configuration?.GetValue<bool?>(nameof(UseAuthentication));

        UseAzure = configuration?.GetValue<bool?>(nameof(UseAzure));

        UseCors = configuration?.GetValue<bool?>(nameof(UseCors));

        UseSwagger = configuration?.GetValue<bool?>(nameof(UseSwagger));

        WebApiRoute = new WebApiRoute(configuration, provider);

        WebToken = new WebToken(configuration, provider);
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public string? AllowedHosts { get; private set; } = null!;

    /// <summary>
    /// Authorization for web API based access
    /// </summary>
    public string? ApiKey { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string? ApiSubscriber { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public int? AppConnectTimeout { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string? ApplicationName { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string? AuthenticationMode { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public IConfiguration? Configuration { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public ConnectionType ConnectionType { get; private set; } = ConnectionType.Memory;

    /// <summary>
    /// 
    /// </summary>
    public int? CookieExpiration { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public ActiveEnvironment Environment { get; set; } = ActiveEnvironment.Default;

    /// <summary>
    /// 
    /// </summary>
    public int? RequestTimeout { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider? ServiceProvider { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public int? SessionTimeout { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string? SwaggerVersion { get; private set; } = null;

    /// <summary>
    /// 
    /// </summary>
    public bool? UseAuthentication { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public bool? UseAzure { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public bool? UseCors { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public bool? UseSwagger { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public IWebApiRoute? WebApiRoute { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public IWebToken? WebToken { get; private set; }

    #endregion

}
