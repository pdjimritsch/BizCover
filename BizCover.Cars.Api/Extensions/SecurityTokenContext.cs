using BizCover.Cars.Network.ServiceModels;

namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// 
/// </summary>
public static partial class SecurityTokenContext
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder? ConfigureSecurityTokens(this WebApplicationBuilder? builder, out JwtSettings? settings)
    {
        settings = new JwtSettings();

        builder?.Configuration.Bind("JsonWebTokenKeys", settings);

        builder?.Services.AddSingleton(settings);

        return builder;
    }

    #endregion
}
