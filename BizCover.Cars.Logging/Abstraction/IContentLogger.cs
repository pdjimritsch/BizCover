using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Logging.Abstraction;

/// <summary>
/// 
/// </summary>
public interface IContentLogger
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    IConfiguration Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    ILogger Logger { get; }

    /// <summary>
    /// 
    /// </summary>
    IServiceProvider? ServiceProvider { get; }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    bool IsEnabled(LogLevel level);

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    void LogCritical(Exception? violation, string? message = null);

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    void LogCritical(string? message = null);

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
    void LogCritical(EventId id, Exception? violation, string? message = null);

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    void LogError(Exception? violation, string? message = null);

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    void LogError(string? message = null);

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
    void LogError(EventId id, Exception? violation, string? message = null);

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
    void LogIssue(LogLevel level, Exception? violation, string? message = null);

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
    void LogIssue(EventId id, LogLevel level, Exception? violation, string? message = null);

    /// <summary>
    /// Logs the provided message as complementary information.
    /// </summary>
    /// <param name="message">
    /// Informative message
    /// </param>
    /// <param name="level">
    /// Log reporting level that complies to either information, trace or debug
    /// </param>
    void LogMessage(string message, LogLevel level = LogLevel.Information);

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
    void LogMessage(EventId id, string message, LogLevel level = LogLevel.Information);

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="violation">
    /// Encountered violation
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    void LogWarning(Exception? violation, string? message = null);

    /// <summary>
    /// Logs the trace message.
    /// </summary>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    void LogTrace(string? message = null);

    /// <summary>
    /// Logs the trace message.
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
    void LogTrace(EventId id, Exception? violation, string? message = null);

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
    void LogWarning(EventId id, Exception? violation, string? message = null);

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    void LogWarning(string? message = null);

    /// <summary>
    /// Logs the encountered violation to the specific error container.
    /// </summary>
    /// <param name="id">
    /// Event log identifier
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    void LogWarning(EventId id, string? message = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="category"></param>
    void SetCategory(string category);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    void SetCategory<T>();

    #endregion
}
