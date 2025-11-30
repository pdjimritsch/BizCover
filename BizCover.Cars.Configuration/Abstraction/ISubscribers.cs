using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Configuration.Abstraction;

public interface ISubscribers
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    string? ApiSubscriber { get;  }

    /// <summary>
    /// 
    /// </summary>
    int? ApiTimeout { get; }

    /// <summary>
    /// 
    /// </summary>
    IConfiguration? Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 
    /// </summary>
    string? Trigger { get; }

    /// <summary>
    /// 
    /// </summary>
    int? TriggerTimeout { get; }

    #endregion
}
