namespace BizCover.Cars.Logging;

using Abstraction;

/// <summary>
/// 
/// </summary>
public partial class ErrorLogger : IErrorLogger
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private static ErrorLogger? _instance;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    private ErrorLogger() : base() 
    { 
        
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public static IErrorLogger Instance => _instance ?? (_instance = new ErrorLogger());

    /// <summary>
    /// 
    /// </summary>
    public ILoggerFactory? LogFactory { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public ILogger? Logger { get; set; } = null!;

    #endregion

    #region IErrorLogger Members

    /// <summary>
    /// Logs the encountered violation to the specific error container for the specific log level
    /// </summary>
    /// <param name="level">
    /// Log reporting level.
    /// </param>
    /// <param name="message">
    /// Affiliated message (if any);
    /// </param>
    public void Log(LogLevel level, string? message = null)
    {
        if (Logger == null && LogFactory != null)
        {
            Logger = LogFactory.CreateLogger<ErrorLogger>();
        }

        if ((Logger != null) && Logger.IsEnabled(level) && !string.IsNullOrEmpty(message))
        {
            Logger.Log(level, message);
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
    public void Log(LogLevel level, Exception? violation, string? message = null)
    {
        if (Logger == null && LogFactory != null)
        {
            Logger = LogFactory.CreateLogger<ErrorLogger>();
        }

        if ((violation != null) && (Logger != null) && Logger.IsEnabled(level))
        {
            Logger.Log(level, violation, string.IsNullOrEmpty(message) ? violation.Message : message.Trim());
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
        if (Logger == null && LogFactory != null)
        {
            Logger = LogFactory.CreateLogger<ErrorLogger>();
        }

        if ((violation != null) && (Logger != null) && Logger.IsEnabled(LogLevel.Error))
        {
            string error = string.IsNullOrEmpty(message) 
                ? violation.Message : $"{message.Trim()}{Environment.NewLine}{violation.Message}";

            Logger.LogError(violation, error);
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
        if (Logger == null && LogFactory != null)
        {
            Logger = LogFactory.CreateLogger<ErrorLogger>();
        }

        if (!string.IsNullOrEmpty(message))
        {
            Logger?.Log(LogLevel.Error, message.Trim());
        }
    }

    #endregion
}
