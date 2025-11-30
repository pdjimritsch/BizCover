using Microsoft.AspNetCore.Cors.Infrastructure;

using BizCover.Cars.Configuration.Abstraction;

namespace BizCover.Cars.Api.Extensions;

public static partial class CorsExtensions
{
    #region Services

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder">
    /// Cors polict builder
    /// </param>
    /// <param name="configuration">
    /// Configuration settings
    /// </param>
    public static void Configure(this CorsPolicyBuilder? builder, IContextConfiguration? configuration)
    {
        if (builder != null)
        {
            if (configuration?.UseAuthentication ?? false)
            {
                builder.SetIsOriginAllowed((origin) =>
                {
                    if (string.IsNullOrWhiteSpace(origin)) return false;
                    
                    return origin.IndexOf("localhost") >= 0; // change when placed within the cloud
                });
            }
            else
            {
                builder.AllowAnyOrigin();
            }

            if (configuration == null)
            {
                builder.WithHeaders(new string[] { "Origin", "X-Requested-With", "Accept" });
                builder.WithExposedHeaders(new string[] { "Origin", "X-Requested-With", "Accept" });
            }
            else if (string.IsNullOrEmpty(configuration.ApiKey))
            {
                builder.WithHeaders(new string[] { "Origin", "X-Requested-With", "Accept" });
                builder.WithExposedHeaders(new string[] { "Origin", "X-Requested-With", "Accept" });
            }
            else
            {
                builder.WithHeaders(new string[] { "Origin", "Authentication", "X-Requested-With", "Accept" });
                builder.WithExposedHeaders(new string[] { "Origin", "Authentication", "X-Requested-With", "Accept" });
            }

            builder.WithMethods(new string[] { "GET", "POST", "PUT", "DELETE", "OPTIONS", "HEAD" });

            if (configuration?.UseAuthentication ?? false)
            {
                builder.AllowCredentials();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="application"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static WebApplication? EnableCors(this WebApplication? application, IContextConfiguration? configuration)
    {
        if ((application != null) && (configuration != null) && (configuration.UseCors ?? false))
        {
            application.UseCors(options =>
            {
                options.Configure(configuration);
            });
        }

        return application;
    }

    #endregion
}