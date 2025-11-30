using System.Net;
using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Security;
using BizCover.Cars.Security.Abstraction;

namespace BizCover.Cars.Api.Extensions;

public static partial class ApiAccessorContext
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static WebApplicationBuilder? InjectAccessorContext(this WebApplicationBuilder? builder, IContextConfiguration? configuration)
    {
        if (builder != null)
        {
            // register REST based services
            builder.Services.AddSingleton<IAppSecurityContext>(provider =>
            {
                return new AppSecurityContext
                {
                    CheckCertificateRevocationList = true,
                    SecurityProtocol = SecurityProtocolType.Tls13,
                };
            });

            // register HTTP client
            builder.SetHttpClient(configuration);

            builder.Services.AddHttpContextAccessor();
        }

        return builder;
    }

    #endregion
}
