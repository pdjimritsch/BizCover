using BizCover.Cars.Configuration.Abstraction;
using BizCover.Cars.Configuration.Enumerations;
using BizCover.Cars.Data.Factory;
using BizCover.Cars.Data.Factory.Abstraction;
using BizCover.Cars.Repository;
using BizCover.Cars.Repository.Abstraction;

namespace BizCover.Cars.Api.Extensions;

static partial class ServiceContext
{
    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static WebApplicationBuilder? SetRepositories(this WebApplicationBuilder? builder, IContextConfiguration? configuration)
    {
        if (builder != null)
        {
            if (configuration != null)
            {
                if (configuration.ConnectionType == ConnectionType.Memory)
                {
                    if ((configuration.Environment == ActiveEnvironment.Default) ||
                        (configuration.Environment == ActiveEnvironment.Development))
                    {
                        builder.Services.AddTransient<ITokenStore>(provider => { return TokenStore.Instance; });

                        builder.Services.AddTransient<ICarRepository>(provider =>
                        {
                            return new CarRepository(configuration, provider);
                        });
                    }
                    else if (configuration.Environment == ActiveEnvironment.Staging)
                    {
                        var connector = configuration.Configuration?.GetConnectionString("Staging");

                        if (string.IsNullOrEmpty(connector))
                        {
                            builder.Services.AddTransient<ITokenStore>(provider => { return TokenStore.Instance; });

                            builder.Services.AddTransient<ICarRepository>(provider =>
                            {
                                return new CarRepository(configuration, provider);
                            });

                        }
                        else
                        {
                            builder.Services.AddTransient<IDataManager>(provider => 
                            {
                                return new DataManager(new VaultContext(connector.Trim()));
                            });

                            builder.Services.AddTransient<ICarRepository>(provider => 
                            {
                                var dtm = provider.GetRequiredService<IDataManager>();

                                return new CarRepository(dtm, configuration, provider);
                            });
                        }
                    }
                    else if (configuration.Environment == ActiveEnvironment.Production)
                    {
                        var connector = configuration.Configuration?.GetConnectionString("Production");

                        if (string.IsNullOrEmpty(connector))
                        {
                            builder.Services.AddTransient<ITokenStore>(provider => { return TokenStore.Instance; });

                            builder.Services.AddTransient<ICarRepository>(provider =>
                            {
                                return new CarRepository(configuration, provider);
                            });

                        }
                        else
                        {
                            builder.Services.AddTransient<IDataManager>(provider =>
                            {
                                return new DataManager(new VaultContext(connector.Trim()));
                            });

                            builder.Services.AddTransient<ICarRepository>(provider =>
                            {
                                var dtm = provider.GetRequiredService<IDataManager>();

                                return new CarRepository(dtm, configuration, provider);
                            });
                        }
                    }
                }
            }
        }

        return builder;
    }

    #endregion
}
