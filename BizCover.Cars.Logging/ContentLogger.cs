using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace BizCover.Cars.Logging;
using Abstraction;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true)]
public sealed partial class ContentLogger : IContentLogger
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 
    /// </summary>
    private readonly IServiceProvider? _serviceProvider;

    /// <summary>
    /// 
    /// </summary>
    private ILogger _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="provider"></param>
    public ContentLogger(IConfiguration configuration, IServiceProvider? provider = null) : base()
    {
        _configuration = configuration;

        _serviceProvider = provider;

        _logger = LoggerFactory.Create(builder =>
            Configure(builder)).CreateLogger<ContentLogger>();
    }

    #endregion

    #region IContentLogger Properties

    /// <summary>
    /// 
    /// </summary>
    public IConfiguration Configuration => _configuration;

    /// <summary>
    /// 
    /// </summary>
    public ILogger Logger => _logger;

    /// <summary>
    /// 
    /// </summary>
    public IServiceProvider? ServiceProvider => _serviceProvider;

    #endregion

    #region IContentLogger Members

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel level)
    {
        return _logger.IsEnabled(level);
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogCritical(Exception? violation, string? message = null)
    {
        if (IsEnabled(LogLevel.Critical))
        {
            _logger.LogCritical(violation, message);
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogCritical(string? message = null)
    {
        if (IsEnabled(LogLevel.Critical))
        {
            _logger.LogCritical(message);
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="id">
    /// Event log identifier
    /// </param>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogCritical(EventId id, Exception? violation, string? message = null)
    {
        if (IsEnabled(LogLevel.Critical))
        {
            _logger.LogCritical(id, violation, message);
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogError(Exception? violation, string? message = null)
    {
        if (IsEnabled(LogLevel.Error))
        {
            _logger?.LogError(violation, message);
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogError(string? message = null)
    {
        if (IsEnabled(LogLevel.Error))
        {
            _logger?.LogError(message);
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="id">
    /// Event log identifier
    /// </param>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogError(EventId id, Exception? violation, string? message = null)
    {
        if (IsEnabled(LogLevel.Error))
        {
            _logger?.LogError(id, violation, message);
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container for the specific log level
    /// </summary>
    /// <param name="level">
    /// Log reporting level.
    /// </param>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogIssue(LogLevel level, Exception? violation, string? message = null)
    {
        var recorded = false;

        if (!recorded && 
            (level == LogLevel.Critical) && IsEnabled(level))
        {
            _logger?.LogCritical(violation, message);
            recorded = true;
        }

        if (!recorded && 
            (level == LogLevel.Error) && IsEnabled(level))
        {
            _logger?.LogError(violation, message);
            recorded = true;
        }

        if (!recorded && 
            (level == LogLevel.Warning) && IsEnabled(level))
        {
            _logger?.LogWarning(violation, message);
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container for the specific log level
    /// </summary>
    /// <param name="id">
    /// Event log identifier
    /// </param>
    /// <param name="level">
    /// Log reporting level.
    /// </param>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogIssue(EventId id, LogLevel level, Exception? violation, string? message = null)
    {
        var recorded = false;

        if (!recorded && 
            (level == LogLevel.Critical) && IsEnabled(level))
        {
            _logger?.LogCritical(id, violation, message);
            recorded = true;
        }

        if (!recorded && 
            (level == LogLevel.Error) && IsEnabled(level))
        {
            _logger?.LogError(id, violation, message);
            recorded = true;
        }

        if (!recorded && 
            (level == LogLevel.Warning) && !IsEnabled(level))
        {
            _logger?.LogWarning(id, violation, message);
        }
    }

    /// <summary>
    /// Logs the provided message as complementary information.
    /// </summary>
    /// </param>
    /// <param name="message">
    /// Informative message
    /// </param>
    /// <param name="level">
    /// Log reporting level that complies to either information, trace or debug
    /// </param>
    public void LogMessage(string message, LogLevel level = LogLevel.Information)
    {
        var recorded = false;

        if (!recorded & 
            (level == LogLevel.Debug) && IsEnabled(level))
        {
            _logger?.LogDebug(message);
            recorded = true;
        }

        if (!recorded &&
            (level == LogLevel.Trace) && IsEnabled(level))
        {
            _logger?.LogTrace(message);
            recorded = true;
        }

        if (!recorded && 
            (level == LogLevel.Information) && IsEnabled(level))
        {
            _logger?.LogInformation(message);
        }
    }

    /// <summary>
    /// Logs the provided message as complementary information.
    /// </summary>
    /// <param name="id">
    /// Event log identifier
    /// </param>
    /// <param name="message">
    /// Informative message
    /// </param>
    /// <param name="level">
    /// Log reporting level that complies to either information, trace or debug
    /// </param>
    public void LogMessage(EventId id, string message, LogLevel level = LogLevel.Information)
    {
        var recorded = false;

        if (!recorded && 
            (level == LogLevel.Debug) && IsEnabled(level))
        { 
            _logger?.LogDebug(id, message);
            recorded = true;
        }

        if (!recorded && 
            (level == LogLevel.Trace) && IsEnabled(level))
        { 
            _logger?.LogTrace(id, message);
            recorded = true;
        }

        if (!recorded &&
            (level == LogLevel.Information) && IsEnabled(level)) 
        { 
            _logger?.LogInformation(id, message); 
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    public void LogTrace(string? message)
    {
        if (IsEnabled(LogLevel.Trace))
        {
            _logger?.LogTrace(message);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="message"></param>
    public void LogTrace(EventId id, Exception? violation, string? message)
    {
        if (IsEnabled(LogLevel.Trace))
        {
            if (violation == null && !string.IsNullOrEmpty(message))
            {
                _logger?.LogTrace(id, message);
            }
            else if (violation != null && !string.IsNullOrEmpty(message))
            {
                _logger?.LogTrace(id, violation, message);
            }
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogWarning(Exception? violation, string? message = null)
    {
        if (IsEnabled(LogLevel.Warning))
        {
            _logger?.LogWarning(violation, message);
        }
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="id">
    /// Event log identifier
    /// </param>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogWarning(EventId id, Exception? violation, string? message = null)
    {
        if (IsEnabled(LogLevel.Warning))
            _logger?.LogWarning(id, violation, message);
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogWarning(string? message = null)
    {
        if (IsEnabled(LogLevel.Warning))
            _logger?.LogWarning(message);
    }

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="id">
    /// Event log identifier
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void LogWarning(EventId id, string? message = null)
    {
        if (IsEnabled(LogLevel.Warning))
            _logger?.LogWarning(id, message);
    }

    /// <summary>
    /// Assigns the logging configuration to a named category
    /// within the configuration settings
    /// </summary>
    /// <param name="category"></param>
    public void SetCategory(string category)
    {
        if (!string.IsNullOrEmpty(category))
        {
            category = category.Trim();

            _logger = LoggerFactory.Create(builder =>
                Configure(builder)).CreateLogger(category);
        }
    }

    /// <summary>
    /// Assigns the logging configuration to a specific category type
    /// within the configuration settings
    /// </summary>
    /// <param name="category"></param>
    public void SetCategory<T>()
    {
        _logger = LoggerFactory.Create(builder => 
            Configure(builder)).CreateLogger<T>();
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// Configures the required logging service.
    /// </summary>
    /// <param name="builder"></param>
    private void Configure(ILoggingBuilder builder)
    {
        if ((builder != null) && (Configuration != null))
        {
            builder.AddConfiguration(Configuration);

            builder.Configure(options =>
            {
                options.ActivityTrackingOptions = ActivityTrackingOptions.TraceState;
            });
        }
    }

    #endregion
}
