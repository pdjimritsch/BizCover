using System.Net.Http.Headers;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

using BizCover.Cars.Network.Abstraction;

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// Employs basic authentication using username and password
/// </summary>
public partial class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    public static readonly string AuthenticationScheme = nameof(BasicAuthenticationHandler);

    #endregion

    #region Members

    /// <summary>
    /// 
    /// </summary>
    readonly IBasicAuthenticationService? _service;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    /// <param name="encoder"></param>
    /// <param name="clock"></param>
    public BasicAuthenticationHandler
    (
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IBasicAuthenticationService service
    )
    : base(options, logger, encoder)
    {
        _service = service;
    }

    #endregion

    #region Overrides

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var response = AuthenticateResult.Fail(new SecurityException("Invalid Credentials"));

        try
        {
            if (Response != null && Response.Headers.Contains(new KeyValuePair<string, StringValues>("WWW-Authenticate", "Basic")))
            {
                Response.Headers.Append("WWW-Authenticate", "Basic");
            }

            if (!(Request?.Headers.ContainsKey("Authorization") ?? false))
            {
                return await Task.FromResult(AuthenticateResult.Fail(new SecurityException("Authorization was not provided.")));
            }

            AuthenticationHeaderValue? authHeader = null;

            if (Request.Headers.ContainsKey("Authorization"))
            {
                authHeader = AuthenticationHeaderValue.Parse(Request.Headers.Authorization.ToString());
            }

            byte[] credentialBytes = [];

            if (authHeader != null && authHeader?.Parameter != null &&  authHeader.Parameter.Length > 0)
            {
                var span = new Span<byte>(new byte[authHeader.Parameter.Length * 8]);

                if (Convert.TryFromBase64String(authHeader.Parameter, span, out int length) && (length > 0))
                {
                    credentialBytes = span.ToArray().Take(length).ToArray();
                }
                else if (Encoding.UTF8.TryGetBytes(new ReadOnlySpan<char>(authHeader.Parameter.ToCharArray()), span, out int size) && (size > 0))
                {
                    credentialBytes = span.ToArray().Take(size).ToArray();
                }
            }

            string? content = Encoding.UTF8.GetString(credentialBytes);

            if (!string.IsNullOrEmpty(content))
            {
                var credentials = content.Trim().Split(Convert.ToChar(":"), 2, StringSplitOptions.RemoveEmptyEntries);

                var username = credentials.Length > 0 ? credentials[0] : string.Empty;

                var password = credentials.Length > 1 ? credentials[1] : string.Empty;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    return await Task.FromResult(AuthenticateResult.Fail(new SecurityException("The username or the password was not provided")));
                }

                using JoinableTaskContext ctx = new();

                JoinableTaskFactory factory = new(ctx);

                await factory.RunAsync(async () =>
                {
                    if (_service is not null)
                    {
                        var account = await _service.AuthenticateAsync(username, password);

                        if (account != null)
                        {
                            var claims = new Claim[]
                            {
                                new Claim(ClaimTypes.NameIdentifier, account?.Id ?? string.Empty),
                                new Claim(ClaimTypes.Name, account?.Name ?? string.Empty),
                            };

                            var identity = new ClaimsIdentity(claims, Scheme.Name);

                            var principal = new ClaimsPrincipal(identity);

                            var ticket = new AuthenticationTicket(principal, Scheme.Name);

                            response = AuthenticateResult.Success(ticket);
                        }
                        else
                        {
                            response = AuthenticateResult.Fail(new SecurityException("The username or the password was not authenticated."));
                        }
                    }

                    await Task.Yield();
                });
            }
        }
        catch (Exception violation)
        {
            response = AuthenticateResult.Fail(violation);
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    protected override Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        return base.HandleChallengeAsync(properties);
    }

    #endregion
}
