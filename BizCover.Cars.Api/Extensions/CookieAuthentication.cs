using System.Security.Claims;
using System.Text.Encodings.Web;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// 
/// </summary>
public sealed partial class CookieAuthentication : CookieAuthenticationHandler
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public static readonly string AuthenticationScheme = "Cookie";

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    /// <param name="encoder"></param>
    public CookieAuthentication
    (
        IOptionsMonitor<CookieAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder
    )
    : base(options, logger, encoder)
    {
    }

    #endregion

    #region Overrides

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var response =  await base.HandleAuthenticateAsync();

        if (response?.Succeeded ?? false) return await Task.FromResult(response);

        var identity = Context.User.Identity ?? new ClaimsIdentity(new List<Claim>(Context.User.Claims));

        var context_user = new ClaimsPrincipal(identity);

        var scheme = identity.Name ?? "customer";

        await Context.SignInAsync(scheme , context_user);

        var ticket = new AuthenticationTicket(context_user, scheme);

        return AuthenticateResult.Success(ticket);
    }

    #endregion
}
