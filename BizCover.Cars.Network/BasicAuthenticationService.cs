using System.ComponentModel;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using BizCover.Cars.Configuration.Abstraction;

namespace BizCover.Cars.Network;
using Abstraction;
using ServiceModels;

/// <summary>
/// 
/// </summary>
[ImmutableObject(true)]
public sealed partial class BasicAuthenticationService : IBasicAuthenticationService
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    readonly IServiceProvider? _provider;

    /// <summary>
    /// 
    /// </summary>
    readonly IContextConfiguration? _configuration;

    /// <summary>
    /// 
    /// </summary>
    readonly IHttpCommunication? _communicator;

    /// <summary>
    /// 
    /// </summary>
    readonly ILogger? _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="provider"></param>
    public BasicAuthenticationService(
        IServiceProvider? provider, IContextConfiguration? configuration, IHttpCommunication? communicator = null, ILogger? logger = null) : base()
    {
        _provider = provider;
        _configuration = configuration;
        _communicator = communicator ?? provider?.GetRequiredService<IHttpCommunication>();
        _logger = logger;
    }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <remarks>
    /// https://codeburst.io/adding-basic-authentication-to-an-asp-net-core-web-api-project-5439c4cf78ee
    /// </remarks>
    /// <returns></returns>
    public async Task<IUserAccount?> AuthenticateAsync(string? username, string? password)
    {
        /*
         * TODO:
         */

        UserAccount? account = default;


        return await Task.FromResult(account);
    }

    #endregion
}
