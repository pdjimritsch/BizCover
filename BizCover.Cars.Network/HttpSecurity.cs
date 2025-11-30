namespace BizCover.Cars.Network;
using Abstraction;

/// <summary>
/// 
/// </summary>
public static partial class HttpSecurity
{
    #region Services

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public static void ApplySecurity(this IAppSecurityContext context)
    {
        if (context == null)
        {
            return;
        }

        //ServicePointManager.CheckCertificateRevocationList =
        //    context.CheckCertificateRevocationList;

        //ServicePointManager.DefaultConnectionLimit =
        //    ServicePointManager.DefaultPersistentConnectionLimit;

        //ServicePointManager.DnsRefreshTimeout = context.DnsRefreshTimeout;

        //ServicePointManager.EnableDnsRoundRobin = context.EnableDnsRoundRobin;

        //ServicePointManager.Expect100Continue = context.Expect100Continue;

        //ServicePointManager.MaxServicePointIdleTime = context.MaxServicePointIdleTime;

        //ServicePointManager.ReusePort = context.ReusePort;

        //ServicePointManager.SecurityProtocol = context.SecurityProtocol;

        //ServicePointManager.ServerCertificateValidationCallback =
        //    context.ServerCertificateValidationCallback;

        //ServicePointManager.UseNagleAlgorithm = context.UseNagleAlgorithm;
    }

    #endregion
}
