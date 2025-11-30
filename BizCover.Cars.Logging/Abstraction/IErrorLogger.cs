namespace BizCover.Cars.Logging.Abstraction;

/// <summary>
/// Logs encountered issues
/// </summary>
public interface IErrorLogger
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    ILoggerFactory? LogFactory { get; set; }

    /// <summary>
    /// 
    /// </summary>
    ILogger? Logger { get; }

    #endregion

    #region Functions

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
    void Log(LogLevel level, string? message = null);

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
    void Log(LogLevel level, Exception? violation, string? message = null);

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

    #endregion
}
