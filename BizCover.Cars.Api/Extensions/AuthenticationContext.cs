using System.Reflection;
using System.Text;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;

using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Network;
using BizCover.Cars.Network.Abstraction;
using BizCover.Cars.Network.Enumerations;
using BizCover.Cars.Network.Extensions;
using BizCover.Cars.Network.ServiceModels;

namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// 
/// </summary>
public static partial class AuthenticationContext
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <param name="browser_url"></param>
    /// <returns></returns>
    public static WebApplicationBuilder? AddAuthenticationContext(this WebApplicationBuilder? builder, IContextConfiguration? configuration, string? url)
    {
        var enabled = configuration?.UseAuthentication ?? false;

        url = url?.GetBrowserUrl(ref enabled) ?? string.Empty;

        if (enabled)
        {
            var mode = AuthenticationMode.Parse(configuration?.AuthenticationMode);

            string application_name = Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty;

            if (mode == AuthenticationMode.UseJwtBearer)
            {
                JwtSettings? settings = default;

                builder?.ConfigureSecurityTokens(out settings);

                if (configuration == null)
                {
                    var authenticationBuilder = builder?.Services.AddAuthentication(opts =>
                    {
                        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                    }).AddJwtBearer(opts =>
                    {
                        opts.Audience = url;
                        opts.Authority = string.Empty;
                        opts.RequireHttpsMetadata = true;
                        opts.SaveToken = true;
                        opts.TokenValidationParameters = new TokenValidationParameters
                        {
                            ClockSkew = TimeSpan.FromDays(1),
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings?.IssuerSigningKey ?? String.Empty)),
                            RequireExpirationTime = settings?.RequireExpirationTime ?? false,
                            ValidateAudience = settings?.ValidateAudience ?? false,
                            ValidAudience = settings?.ValidAudience ?? string.Empty,
                            ValidIssuer = settings?.ValidIssuer ?? string.Empty,
                            ValidateIssuerSigningKey = settings?.ValidateIssuerSigningKey ?? false,
                            ValidateLifetime = settings?.RequireExpirationTime ?? false
                        };
                    });
                }
                else if (configuration.UseAzure ?? false)
                {
                    var authenticationBuilder = builder?.Services.AddAuthentication(opts =>
                    {
                        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                    }).AddJwtBearer(opts =>
                    {
                        opts.Audience = url;
                        opts.Authority = configuration.ApiSubscriber;
                        opts.RequireHttpsMetadata = true;
                        opts.SaveToken = true;
                        opts.TokenValidationParameters = new TokenValidationParameters
                        {
                            ClockSkew = TimeSpan.FromDays(1),
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings?.IssuerSigningKey ?? String.Empty)),
                            RequireExpirationTime = settings?.RequireExpirationTime ?? false,
                            ValidateAudience = settings?.ValidateAudience ?? false,
                            ValidAudience = settings?.ValidAudience ?? string.Empty,
                            ValidIssuer = settings?.ValidIssuer ?? string.Empty,
                            ValidateIssuerSigningKey = settings?.ValidateIssuerSigningKey ?? false,
                            ValidateLifetime = settings?.RequireExpirationTime ?? false
                        };
                    });
                }
                else
                {
                    var authenticationBuilder = builder?.Services.AddAuthentication(opts =>
                    {
                        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                    }).AddJwtBearer(opts =>
                    {
                        opts.Audience = url;
                        opts.Authority = configuration?.ApiSubscriber ?? string.Empty;
                        opts.RequireHttpsMetadata = true;
                        opts.SaveToken = true;
                        opts.TokenValidationParameters = new TokenValidationParameters
                        {
                            ClockSkew = TimeSpan.FromDays(1),
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration?.WebToken?.IssuerSigningKey ?? string.Empty)),
                            RequireExpirationTime = configuration?.WebToken?.RequireExpirationTime ?? false,
                            ValidateAudience = configuration?.WebToken?.ValidateAudience ?? false,
                            ValidAudience = configuration?.WebToken?.ValidAudience ?? string.Empty,
                            ValidIssuer = configuration?.WebToken?.ValidIssuer ?? string.Empty,
                            ValidateIssuerSigningKey = configuration?.WebToken?.ValidateIssuerSigningKey ?? false,
                            ValidateLifetime = configuration?.WebToken?.RequireExpirationTime ?? false
                        };
                    });
                }
            }
            else if (mode == AuthenticationMode.OAuth2)
            {
                var authenticationBuilder = builder?.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie("customer").AddCookie("visitor")
                    .AddOAuth(OAuthDefaults.DisplayName, opts =>
                    {
                        opts.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        opts.ClientId = string.Empty;
                        opts.ClientSecret = string.Empty;
                        opts.AuthorizationEndpoint = "https://localhost:7270/oauth/authorize";
                        opts.TokenEndpoint = "https://localhost:7270/oauth/token";
                        opts.CallbackPath = "oauth/callback";
                        opts.UsePkce = true;
                        //opts.ClaimActions.MapJsonKey("sub_claim", "sub_claim");
                        opts.Events.OnCreatingTicket = async (ctx) =>
                        {
                            string[] pay_load_base64 = null!;
                            try
                            {
                                //TODO:
                            }
                            catch (Exception)
                            {
                                pay_load_base64 = null!;
                            }

                            if (pay_load_base64 != null! && pay_load_base64.Length > 0)
                            {
                                var value = (ctx.AccessToken ?? string.Empty).Split('.')[1];
                                var pay_load_json = Base64UrlTextEncoder.Decode(value);
                                var pay_load = JsonDocument.Parse(pay_load_json);
                                ctx.RunClaimActions(pay_load.RootElement);
                            }
                            await Task.Yield();
                        };
                    });
            }
            else if (mode == AuthenticationMode.UseLocalApi)
            {
                /*
                 * https://docs.identityserver.io/en/latest/topics/add_apis.html
                 */

                var authenticationBuilder = builder?.Services.AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                });
            }
            else if (mode == AuthenticationMode.Cookie)
            {
                var authenticationBuilder = builder?.Services.AddAuthentication(opts =>
                {

                })
                .AddCookie("customer", o =>
                {
                    o.Cookie.Domain = url;
                    o.Cookie.Expiration = TimeSpan.FromMinutes(15);
                    o.Cookie.HttpOnly = false;
                    o.Cookie.MaxAge = TimeSpan.FromHours(12);
                    o.Cookie.Path = "/";
                    o.Cookie.SameSite = SameSiteMode.Strict;
                    o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                })
                .AddCookie("visitor", o =>
                {
                    o.Cookie.Domain = url;
                    o.Cookie.Expiration = TimeSpan.FromMinutes(15);
                    o.Cookie.HttpOnly = false;
                    o.Cookie.MaxAge = TimeSpan.FromHours(12);
                    o.Cookie.Path = "/";
                    o.Cookie.SameSite = SameSiteMode.Strict;
                    o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                });
            }
            else if (mode == AuthenticationMode.Basic)
            {
                /*
                 * Basic authentication is an Authentication Scheme built into the HTTP protocol which uses a simple UserName and Passwords 
                 * to access a restricted resource. 
                 * 
                 * These UserName and Passwords are translated to standard “Authorization” headers using Bas64 encoding.
                 */

               builder?.Services.AddScoped<IBasicAuthenticationService>(provider =>
                {
                    var settings = provider.GetRequiredService<IContextConfiguration>();

                    var communicator = provider.GetRequiredService<IHttpCommunication>();

                    return new BasicAuthenticationService(provider, settings, communicator);
                });

                var authenticationBuilder = builder?.Services.AddAuthentication(opts =>
                {
                    opts.DefaultAuthenticateScheme = "BasicAuthentication";
                })
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", options =>
                {
                    //TODO:

                });

            }

            builder?.Services.AddSingleton<IAuthenticationSchemeProvider, AuthenticationSchemeManager>();

            builder?.Services.AddSingleton<IAuthenticationHandlerProvider, AuthenticationHandlerProvider>(); 
        }
        else
        {
            builder?.Services.AddDistributedMemoryCache(options => 
            { 
                options.ExpirationScanFrequency = TimeSpan.FromDays(1);
            });

            builder?.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        return builder;
    }

    #endregion
}