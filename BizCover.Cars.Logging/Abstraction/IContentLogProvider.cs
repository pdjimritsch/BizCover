namespace BizCover.Cars.Logging.Abstraction;

/// <summary>
/// 
/// </summary>
public interface IContentLogProvider : ILoggerProvider
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    IContentLogger Logger { get; }

    #endregion
}
