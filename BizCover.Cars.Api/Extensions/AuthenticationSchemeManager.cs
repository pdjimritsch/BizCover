using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

using BizCover.Cars.Common;

namespace BizCover.Cars.Api.Extensions;

/// <summary>
/// 
/// </summary>
public partial class AuthenticationSchemeManager: AuthenticationSchemeProvider
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    static readonly object? _lock = new();

    #endregion

    #region IAuthenticationSchemeProvider Members

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public AuthenticationSchemeManager(IOptions<AuthenticationOptions> options) : base(options)
    {
        Options = options;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="schemes"></param>
    public AuthenticationSchemeManager(IOptions<AuthenticationOptions> options, IDictionary<string, AuthenticationScheme> schemes)
        : base(options, schemes) 
    {
        Options = options;

        if (schemes?.Any() ?? false)
        {
            foreach (var scheme in schemes) 
            {
                Schemas.TryAdd(scheme.Key, scheme.Value);
            }
        }

    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public IOptions<AuthenticationOptions> Options { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<AuthenticationScheme, IAuthenticationRequestHandler> RequestHandlers { get; set; } = [];

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, AuthenticationScheme> Schemas { get; set; } = [];

    #endregion

    #region Overrides

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scheme"></param>
    public override void AddScheme(AuthenticationScheme scheme)
    {
        lock(_lock ?? new object())
        {
            if ((scheme != null) && !Schemas.ContainsKey(scheme.Name))
            {
                if (!TryAddScheme(scheme))
                {
                    throw new InvalidOperationException("Scheme already exists: " + scheme.Name);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<AuthenticationScheme>> GetAllSchemesAsync()
        => await Task.FromResult(Schemas.Select(x => x.Value));

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationScheme?> GetDefaultAuthenticateSchemeAsync()
        => (Options?.Value?.DefaultScheme?.IsContentEmpty() ?? true) 
        ? await GetDefaultSchemeAsync() : await GetSchemeAsync(Options?.Value?.DefaultScheme ?? string.Empty);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationScheme?> GetDefaultChallengeSchemeAsync()
        => (Options?.Value?.DefaultChallengeScheme?.IsContentEmpty() ?? true)
        ? await GetDefaultSchemeAsync() : await GetSchemeAsync(Options?.Value?.DefaultChallengeScheme ?? string.Empty);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationScheme?> GetDefaultForbidSchemeAsync()
        => (Options?.Value?.DefaultForbidScheme?.IsContentEmpty() ?? true)
        ? await GetDefaultSchemeAsync() : await GetSchemeAsync(Options?.Value?.DefaultForbidScheme ?? string.Empty);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationScheme?> GetDefaultSignInSchemeAsync()
        => (Options?.Value?.DefaultSignInScheme?.IsContentEmpty() ?? true)
        ? await GetDefaultSchemeAsync() : await GetSchemeAsync(Options?.Value?.DefaultSignInScheme ?? string.Empty);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationScheme?> GetDefaultSignOutSchemeAsync()
        => (Options?.Value?.DefaultSignOutScheme?.IsContentEmpty() ?? true)
        ? await GetDefaultSchemeAsync() : await GetSchemeAsync(Options?.Value?.DefaultSignOutScheme ?? string.Empty);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<IEnumerable<AuthenticationScheme>> GetRequestHandlerSchemesAsync()
        => await Task.FromResult(RequestHandlers.Select(x => x.Key));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public override async Task<AuthenticationScheme?> GetSchemeAsync(string name)
        => await Task.FromResult(name.IsContentEmpty() ? null : Schemas.ContainsKey(name) ? Schemas[name] : null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public override void RemoveScheme(string name)
    {
        lock(_lock ?? new object())
        {
            if (name.IsContentEmpty()) return;

            if (!Schemas.ContainsKey(name)) return;

            var scheme = Schemas[name];

            if ((scheme != null) && RequestHandlers.Remove(scheme))
            {
                Schemas.Remove(name);
            }

        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scheme"></param>
    /// <returns></returns>
    public override bool TryAddScheme(AuthenticationScheme scheme)
    {
        lock(_lock ?? new object())
        {
            if (scheme == null) return false;

            if ((scheme != null) && !Schemas.ContainsKey(scheme.Name))
            {
                Schemas[scheme.Name] = scheme;
            }

            if ((scheme != null) && Schemas.ContainsKey(scheme.Name) &&
                typeof(IAuthenticationRequestHandler).IsAssignableFrom(scheme.HandlerType))
            {
                //TODO:

                //RequestHandlers.TryAdd(scheme, null);
            }

            return (scheme != null) && Schemas.ContainsKey(scheme.Name);
        }
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private async Task<AuthenticationScheme?> GetDefaultSchemeAsync()
        => (Options?.Value?.DefaultScheme?.IsContentEmpty() ?? true) 
        ?  await Task.FromResult(default(AuthenticationScheme)) 
        : await GetSchemeAsync(Options?.Value?.DefaultScheme ?? string.Empty);

    #endregion
}
