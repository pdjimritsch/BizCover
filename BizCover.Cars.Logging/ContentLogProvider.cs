using System.ComponentModel;

using Microsoft.Extensions.Configuration;

namespace BizCover.Cars.Logging;
using Abstraction;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true)]
public sealed partial class ContentLogProvider : IContentLogProvider
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly IContentLogger _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="provider"></param>
    public ContentLogProvider(IConfiguration configuration, IServiceProvider? provider = null) : base()
    {
        _logger = new ContentLogger(configuration, provider);
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public IContentLogger Logger => _logger;

    #endregion

    #region IErrorLogProvider Members

    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
        _logger.SetCategory(categoryName);
        return _logger.Logger;
    }

    #endregion

    #region IAsyncDisposable Members

    /// <summary>
    /// 
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await Task.Yield();
    }

    #endregion

    #region IDisposable Members

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
    }

    #endregion
}
