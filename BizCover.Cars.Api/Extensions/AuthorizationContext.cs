using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Network.Enumerations;

namespace BizCover.Cars.Api;

using Extensions;

/// <summary>
/// 
/// </summary>
public static partial class AuthorizationContext
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder? AddAuthorizationContext(this WebApplicationBuilder? builder, bool secure_site_enabled)
    {
        var enabled = builder?.Configuration.GetValue<bool?>(nameof(IContextConfiguration.UseAuthentication));

        var appname = builder?.Configuration.GetValue<string?>(nameof(IContextConfiguration.ApplicationName) ?? AppDomain.CurrentDomain.FriendlyName);

        if (secure_site_enabled || (enabled ?? false))
        {
            var mode =
                AuthenticationMode.Parse(builder?.Configuration.GetValue<string?>(nameof(IContextConfiguration.AuthenticationMode)));

            if (mode == AuthenticationMode.Basic)
            {
                /*
                 * Basic authentication is an Authentication Scheme built into the HTTP protocol which uses a simple UserName and Passwords 
                 * to access a restricted resource. 
                 * 
                 * These UserName and Passwords are translated to standard “Authorization” headers using Bas64 encoding.
                 */

                builder?.Services.AddAuthorization(opts =>
                {
                    opts.AddPolicy("BasicAuthentication", b =>
                    {
                        b.AddAuthenticationSchemes("BasicAuthentication", "customer", "visitor").RequireAuthenticatedUser();
                    });
                });
            }
            else if (mode == AuthenticationMode.Cookie)
            {
                /* Cookie authentication */

                builder?.Services.AddAuthorization(opts =>
                {
                    opts.AddPolicy(appname ?? string.Empty, policy =>
                    {
                        policy.Requirements.Add(new AppServiceRequirement());
                    });

                    opts.AddPolicy("customer", policy =>
                    {
                        policy.Requirements.Add(new AppServiceRequirement());
                    });

                });

            }
            else if (mode == AuthenticationMode.UseIdentityWebApi)
            {
                builder?.Services.AddAuthorization(opts =>
                {
                    opts.AddPolicy(appname ?? string.Empty, policy =>
                    {
                        policy.Requirements.Add(new AppServiceRequirement());
                    });

                });
            }
            else if (mode == AuthenticationMode.UseJwtBearer)
            {
                builder?.Services.AddAuthorization(opts =>
                {
                    opts.AddPolicy(appname ?? string.Empty, policy =>
                    {
                        policy.Requirements.Add(new AppServiceRequirement());
                    });
                });
            }
            else if (mode == AuthenticationMode.UseOpenIdConnect)
            {
                builder?.Services.AddAuthorization(opts =>
                {
                    opts.AddPolicy(appname ?? string.Empty, policy =>
                    {
                        policy.Requirements.Add(new AppServiceRequirement());
                    });
                });
            }
            else if (mode == AuthenticationMode.OAuth2)
            {
                //TODO;
            }
        }

        return builder;
    }

    #endregion
}
