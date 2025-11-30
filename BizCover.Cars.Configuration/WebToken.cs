using Microsoft.Extensions.Configuration;
namespace BizCover.Cars.Configuration;
using Abstraction;

public partial class WebToken : IWebToken
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public const string SectionName = "JsonWebTokenKeys";

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="provider"></param>
    public WebToken(IConfiguration? configuration, IServiceProvider? provider = null) : base()
    {
        Configuration = configuration;

        IssuerSigningKey = configuration?.GetValue<string?>($"{SectionName}:{nameof(IssuerSigningKey)}");

        RequireExpirationTime = configuration?.GetValue<bool?>($"{SectionName}:{nameof(RequireExpirationTime)}");

        ServiceProvider = provider;

        ValidIssuer = configuration?.GetValue<string?>($"{SectionName}:{nameof(ValidIssuer)}");

        ValidateAudience = configuration?.GetValue<bool?>($"{SectionName}:{nameof(ValidateAudience)}");

        ValidAudience = configuration?.GetValue<string?>($"{SectionName}:{nameof(ValidAudience)}");

        ValidateIssuer = configuration?.GetValue<bool?>($"{SectionName}:{nameof(ValidateIssuer)}");

        ValidateIssuerSigningKey = configuration?.GetValue<bool?>($"{SectionName}:{nameof(ValidateIssuerSigningKey)}");

        ValidateLifetime = configuration?.GetValue<bool?>($"{SectionName}:{nameof(ValidateLifetime)}");
    }

    #endregion

    #region IWebToken Members

    /// <summary>
    /// 
    /// </summary>
    public IConfiguration? Configuration { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string? IssuerSigningKey { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? RequireExpirationTime { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider? ServiceProvider { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ValidIssuer { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? ValidateAudience { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public string? ValidAudience { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? ValidateIssuer { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? ValidateIssuerSigningKey { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? ValidateLifetime { get; private set; }

    #endregion
}