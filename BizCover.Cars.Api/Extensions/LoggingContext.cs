using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Logging.EventLog;

using BizCover.Cars.Logging;
using BizCover.Cars.Logging.Abstraction;

using ExtendedLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// 
/// </summary>
public static partial class LoggingContext
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="application_root"></param>
    /// <param name="browser_url"></param>
    /// <returns></returns>
    public static void AddLoggers
    (
        this IServiceCollection services,
        IConfiguration configuration,
        string? application_root,
        string? browser_url
    )
    {
        services?.AddLogging(builder => 
            builder?.ConfigureLogging(configuration));

        services?.AddSingleton(provider => LoggerFactory.Create(configure =>
        {
            configure.AddConsole(opts => opts.LogToStandardErrorThreshold = ExtendedLogLevel.Error);
            configure.AddDebug();
            #pragma warning disable CA1416
            configure.AddEventLog(new EventLogSettings 
            { 
                LogName= nameof(Vault.Api),
                MachineName = Environment.MachineName,
                SourceName = "Explorer",
            });
            #pragma warning restore CA1416
        }));

        var logname = Assembly.GetExecutingAssembly().GetName().Name ?? nameof(Vault.Api);
        
        services?.AddSingleton(provider =>
            provider.GetRequiredService<ILoggerFactory>().CreateLogger(logname));

        services?.AddSingleton<IContentLogProvider>(provider =>
        {
            return new ContentLogProvider(configuration, provider);
        });

        services?.AddSingleton(provider =>
        {
            var instance = ErrorLogger.Instance;

            instance.LogFactory = provider.GetRequiredService<ILoggerFactory>();

            return instance;
        });

        services?.AddHttpLogging(options =>
        {
            options.LoggingFields = HttpLoggingFields.All;
            options.RequestHeaders.Add("X-API-Key");
            options.ResponseHeaders.Remove("Content-Type");
            options.MediaTypeOptions.AddText("application/json", Encoding.UTF8);
            options.RequestBodyLogLimit = 4096;
            options.ResponseBodyLogLimit = 4096;
        });

        services?.AddSingleton(provider =>
        {
            var instance = ErrorLogger.Instance;

            instance.LogFactory = provider.GetRequiredService<ILoggerFactory>();

            return instance;

        });
    }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static IErrorLogger? GetErrorLogger(this IServiceProvider? provider)
    {
        if (provider != null)
        {
            return provider.GetRequiredService<IErrorLogger>();
        }

        return null;
    }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    private static void ConfigureLogging(this ILoggingBuilder? builder, IConfiguration configuration)
    {
        builder?.AddConfiguration(configuration)
            .AddConsole(opts => opts.LogToStandardErrorThreshold = ExtendedLogLevel.Error)
            .AddDebug();

        #pragma warning disable CA1416
        builder?.AddEventLog(CreateEventLog());
        #pragma warning restore CA1416
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [SupportedOSPlatform("windows")]
    private static EventLogSettings CreateEventLog()
        => new EventLogSettings
        {
            LogName = nameof(Vault.Api),
            MachineName = Environment.MachineName,
            SourceName = "Explorer",
        };

    #endregion
}
