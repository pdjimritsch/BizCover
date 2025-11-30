namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// 
/// </summary>
public static partial class ApplicationEventContext
{
    #region Events

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lifetime"></param>
    /// <param name="application"></param>
    /// <returns></returns>
    public static IHostApplicationLifetime? Register(this IHostApplicationLifetime? lifetime, WebApplication? application)
    {
        lifetime?.ApplicationStarted.Register(() =>
        {

        });

        lifetime?.ApplicationStopping.Register(() =>
        {

        });

        lifetime?.ApplicationStopped.Register(() =>
        {

        });

        return lifetime;
    }

    #endregion
}
