using BizCover.Cars.Common;
using BizCover.Cars.Configuration;
using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Configuration.Enumerations;
using BizCover.Cars.Network;
using BizCover.Cars.Network.Abstraction;
using BizCover.Cars.Network.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.RateLimiting;
using System.Net;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using CoreJsonOption = Microsoft.AspNetCore.Http.Json;

namespace BizCover.Cars.Api;

using Extensions;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.Extensions.DependencyInjection;
using Middleware;

/// <summary>
/// 
/// </summary>
public static partial class ApiGenerator
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static WebApplication? BuildApi(
        this WebApplicationBuilder? builder, IContextConfiguration? configuration)
    {
        WebApplication? application = default;

        if (builder == null) return application;

        application = builder.Build();

        if (application != null && application.Environment.IsDevelopment() && (configuration?.UseSwagger ?? false))
        {
            application.UseSwagger();
            application.UseSwaggerUI();
        }

        if ((application != null) && !application.Environment.IsDevelopment())
        {
            application?.UseHttpsRedirection();
        }

        if (application != null)
        {
            application.UseRateLimiter();

            application.UseStaticFiles();

            application.UseRouting();

            application.EnableCors(configuration);
        }

        if ((configuration?.UseAuthentication ?? false) && (application != null))
        {
            application.UseAuthorization();
            application.UseAuthentication();
        }
        else if (!(configuration?.UseAuthentication ?? false) && (application != null))
        {
            application.UseSession();
        }

        if (application != null)
        {
            if (!application.Environment.IsDevelopment())
            {
                application.UseExceptionHandler("/Error");

                /**
                 * HSTS => HTTP(s) Strict Transport Security
                 * Is a method by which your application server can tell clients to use 
                 * a secure connection when sending requests.
                 */
                application.UseHsts();

                application.UseHttpsRedirection();
            }

            application.UseStaticFiles();

            application.UseRequestTimeouts();

            /* routing guards */
            application
                .UseAuthorization()
                .UseAntiforgery()
                .UseMiddleware<ApiKeyMiddleware>(application.Environment)
                .UseMiddleware<ExceptionHandlerMiddleware>();
        }

        return application;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IContextConfiguration? GetApiConfiguration(
        IServiceCollection? collection, IConfiguration? configuration, IHostEnvironment? environment)
    {
        IContextConfiguration? settings = new ContextConfiguration(configuration);
       
        collection?.AddSingleton<IContextConfiguration>(provider => 
        { 
            settings = new ContextConfiguration(configuration, provider);

            settings = SetEnvironment(settings, environment);
            
            return settings ?? new ContextConfiguration(configuration, provider);
        });

        return settings;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static RequestTimeoutPolicy? GetRequestDefaultPolicy(IContextConfiguration? configuration)
    {
        if (configuration == null)
        {
            return new RequestTimeoutPolicy
            {
                Timeout = TimeSpan.FromSeconds(30),
                TimeoutStatusCode = (int)HttpStatusCode.GatewayTimeout
            };
        }
        else if ((configuration != null) && (configuration.RequestTimeout == null))
        {
            return new RequestTimeoutPolicy
            {
                Timeout = TimeSpan.FromSeconds(30),
                TimeoutStatusCode = (int)HttpStatusCode.GatewayTimeout
            };
        }
        else if ((configuration != null) && (configuration.RequestTimeout != null))
        {
            return new RequestTimeoutPolicy
            {
                Timeout = (configuration.RequestTimeout.Value <= 0)
                    ? TimeSpan.Zero
                    : TimeSpan.FromMilliseconds(configuration.RequestTimeout.Value),
                TimeoutStatusCode = (int)HttpStatusCode.GatewayTimeout
            };
        }
        return new RequestTimeoutPolicy
        {
            Timeout = TimeSpan.FromSeconds(30),
            TimeoutStatusCode = (int)HttpStatusCode.GatewayTimeout
        };

        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="appRoot"></param>
    /// <param name="environment"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public static WebApplicationOptions GetWebApiOptions(string appRoot, string environment, string[] args)
    {
        return new WebApplicationOptions
        {
            ApplicationName = Assembly.GetExecutingAssembly().GetName().Name,
            Args = args,
            ContentRootPath = Directory.GetCurrentDirectory(),
            EnvironmentName = string.IsNullOrEmpty(environment) ? Environments.Development : environment.Trim(),
            WebRootPath = string.IsNullOrEmpty(appRoot) ? "/" : appRoot.Trim(),
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="environment"></param>
    /// <returns></returns>
    public static IContextConfiguration? SetEnvironment(this IContextConfiguration? configuration, IHostEnvironment? environment)
    {
        if (configuration != null && environment != null)
        {
            if (environment.IsDevelopment())
            {
                configuration.Environment = ActiveEnvironment.Development;
            }
            else if (environment.IsStaging())
            {
                configuration.Environment = ActiveEnvironment.Staging;
            }
            else if (environment.IsProduction())
            {
                configuration.Environment = ActiveEnvironment.Production;
            }
        }
        else if (configuration != null)
        {
            configuration.Environment = ActiveEnvironment.Default;
        }

        return configuration;
    }
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="application"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static WebApplication? SetApiRoutes(this WebApplication? application)
    {
        if (application != null)
        {
            using JoinableTaskContext ctx = new();

            JoinableTaskFactory factory = new(ctx);

            factory.RunAsync(async () =>
            {
                //await application.MapRoutesAsync(application.Configuration);

                await Task.Yield();

            })
            .GetAwaiter().OnCompleted(() => { });     
        }

        return application;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder? SetCors(this IHostApplicationBuilder? builder, IContextConfiguration? configuration)
    {
        if ((builder != null) && (configuration != null) && (configuration.UseCors ?? false))
        {
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.Configure(configuration);
                });
            });
        }
        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder? SetHttpClient(
        this IHostApplicationBuilder builder, IContextConfiguration? configuration)
    {
        builder?.Services.AddHttpClient<IHttpCommunication, HttpCommunication>(nameof(HttpCommunication), (client, provider) =>
        {
            return new HttpCommunication(provider, client)
            {
                ErrorLogger = provider.GetErrorLogger(),
                SecurityContext = provider.GetRequiredService<IAppSecurityContext>() ??
                    new AppSecurityContext
                    {
                        CheckCertificateRevocationList = true,
                        SecurityProtocol = SecurityProtocolType.Tls13,
                    }
            };
        });

        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder? SetJsonOptions(this IHostApplicationBuilder? builder, int maxDepth = 8)
    {
        builder?.Services.Configure<CoreJsonOption.JsonOptions>(options =>
        {
            options.SerializerOptions.AllowOutOfOrderMetadataProperties = false;
            options.SerializerOptions.MaxDepth = maxDepth;
            options.SerializerOptions.PropertyNameCaseInsensitive = false;
            options.SerializerOptions.PropertyNamingPolicy = null;
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.SerializerOptions.WriteIndented = true;

        });

        return builder;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// /** Checks the occurrence of distributed denial of service attacks */
    /// </remarks>
    /// <param name="options"></param>
    public static void SetRequestRateLimit(RateLimiterOptions options)
    {
        if (options != null)
        {
            Func<string, FixedWindowRateLimiterOptions> constraint = (value) =>
            {
                return new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 20,
                    QueueLimit = 0,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    Window = TimeSpan.FromSeconds(30),
                };
            };

            Func<HttpContext, RateLimitPartition<string>> service = (ctx) =>
            {
                var addr = ctx.Connection.RemoteIpAddress;

                string key = StringExtensions.Generate(256);

                if (addr != null)
                {
                    key = addr.ToString();
                }
                else if (ctx != null && ctx.User != null && ctx.User.Identity != null)
                {
                    if (ctx.User.Identity.Name != null)
                    {
                        key = ctx.User.Identity.Name.Trim();
                    }
                }

                return RateLimitPartition.GetFixedWindowLimiter(partitionKey: key, factory: (partition) => constraint(partition));
            };

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(ctx => service(ctx));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder? SetSwaggerUI(this IHostApplicationBuilder? builder, IContextConfiguration? configuration)
    {
        if (builder != null && configuration != null)
        {
            if (builder.Environment.IsDevelopment())
            {
                if (configuration.UseSwagger ?? false)
                {
                    builder.Services.AddEndpointsApiExplorer();

                    builder.Services.AddSwaggerGen(options =>
                    {
                        options.IncludeXmlComments(Assembly.GetExecutingAssembly(), true);
                    });
                }
            }
        }
        return builder;
    }


    /// <summary>
    /// Web API 2 Initialtor
    /// </summary>
    public static void Start(string environment, string[] args)
    {
        string applicationRoot = "/";

        var options = GetWebApiOptions(applicationRoot, environment, args);

        var builder = WebApplication.CreateBuilder(options);

        var configuration = GetApiConfiguration(builder.Services, builder.Configuration, builder.Environment);

        if (configuration != null)
        {
            builder.Services.AddSingleton(configuration);
        }

        string webApiRoute = configuration?.WebApiRoute?.ApiRoute ?? string.Empty;

        var url = builder.Configuration["ASPNETCORE_URLS"];

        builder.Services.AddOptions();

        builder.Services.AddHealthChecks();

        // add forward header
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        builder.Services.AddDistributedMemoryCache(options =>
        {
            options.TrackStatistics = true;
            options.SizeLimit = 1024 * 1024 * 512; // 512 MB
            options.ExpirationScanFrequency = TimeSpan.FromSeconds(60);
            options.TrackLinkedCacheEntries = true;
        });

        // add data protection middleware
        builder.Services.AddDataProtection(options =>
        {
            options.ApplicationDiscriminator = Assembly.GetExecutingAssembly().GetName().Name;
        })
        .AddKeyManagementOptions(options => { options.AutoGenerateKeys = true; })
        .PersistKeysToFileSystem(new DirectoryInfo(AppContext.BaseDirectory))
        .SetApplicationName(AppDomain.CurrentDomain.FriendlyName)
        .SetDefaultKeyLifetime(TimeSpan.FromDays(7));

        /** Checks the occurrence of distributed denial of service attacks */
        builder.Services.AddRateLimiter(options => 
        {
            SetRequestRateLimit(options);
        });

        // add antiforgery middleware
        builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = ApiKeyMiddleware.HEADER_KEY_NAME;
        });

        // configure CORS
        builder.SetCors(configuration);

        if (configuration?.UseAuthentication ?? false)
        {
            builder.AddAuthorizationContext(configuration?.UseAuthentication ?? false);
            builder.AddAuthenticationContext(configuration, url);
        }

        // register security context, context accessor and http client

        builder.InjectAccessorContext(configuration);

        builder.Services.AddControllers(options =>
        {
            options.AllowEmptyInputInBodyModelBinding = true;

            options.OutputFormatters.Add(new HttpNoContentOutputFormatter());

            options.RespectBrowserAcceptHeader = true;
        });

        builder.SetJsonOptions();

        // register web application injectable services
        builder.InjectServices(configuration);

        // add application logging
        builder.Services.AddLoggers(builder.Configuration, applicationRoot, url);

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(15);

            options.IOTimeout = TimeSpan.FromMinutes(5);
        });

        builder.Services.AddRequestTimeouts(options => 
        {
            options.DefaultPolicy = GetRequestDefaultPolicy(configuration);
        });

        builder.SetSwaggerUI(configuration);

        // create the Web Api instance
        var application = builder.BuildApi(configuration);

        if (application != null)
        {
            if (configuration?.UseAuthentication ?? false)
            {
                application.UseAuthorization();

                application.UseAuthentication();
            }

            application.MapControllers();

            application.SetApiRoutes();

            application.UseRouting();

            if (configuration?.UseCors ?? false)
            {
                application.UseCors(policy =>
                {
                    policy.Configure(configuration);
                });
            }

            application.Lifetime.Register(application);

            application.Run();
        }
    }

    #endregion
}
