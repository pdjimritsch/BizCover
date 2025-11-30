using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Configuration.Enumerations;
using BizCover.Cars.Data.Factory.Abstraction;
using BizCover.Cars.Repository;
using BizCover.Cars.Repository.Abstraction;
using BizCover.Cars.Service;
using BizCover.Cars.Service.Abstraction;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Vault.Api.Extensions;

namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// 
/// </summary>
public static partial class ServiceContext
{
    #region Extensions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="application_root"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static WebApplicationBuilder InjectServices(
        this WebApplicationBuilder builder, IContextConfiguration? configuration)
    {
        builder.Services.AddMemoryCache(options => ServiceHelper.GetCacheOptions(options));

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSingleton(TimeProvider.System);

        builder.SetRepositories(configuration);

        builder.SetServices(configuration);

        return builder;
    }

    #endregion
}
