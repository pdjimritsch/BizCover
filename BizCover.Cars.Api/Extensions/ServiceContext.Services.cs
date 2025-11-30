using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Network;
using BizCover.Cars.Network.Abstraction;
using BizCover.Cars.Repository.Abstraction;
using BizCover.Cars.Service;
using BizCover.Cars.Service.Abstraction;

namespace Vault.Api.Extensions;

/// <summary>
/// 
/// </summary>
static partial class ServiceContext
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder SetServices(this WebApplicationBuilder builder, IContextConfiguration? configuration)
    {
        builder.Services.AddTransient<IHttpCommunication>(provider =>
        {
            return new HttpCommunication(provider);
        });

        builder.Services.AddTransient<ICarService>(provider =>
        {
            var repository = provider.GetRequiredService<ICarRepository>();

            return new CarService(repository);
        });

        return builder;
    }

    #endregion
}
