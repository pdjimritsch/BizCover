using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Configuration.Abstraction;

/// <summary>
/// 
/// </summary>
public partial interface IMongoDatabaseOption
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    IConfiguration? Configuration { get; }

    /// <summary>
    /// 
    /// </summary>
    string? Database { get; }

    /// <summary>
    /// 
    /// </summary>
    bool? Enable { get; }

    /// <summary>
    /// 
    /// </summary>
    IServiceProvider? ServiceProvider { get; }

    /// <summary>
    /// 
    /// </summary>
    string? Uri { get; }

    #endregion
}
