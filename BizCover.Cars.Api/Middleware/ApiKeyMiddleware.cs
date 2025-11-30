using System.Net;
using BizCover.Cars.Configuration.Abstraction;
using Microsoft.Extensions.Primitives;

namespace BizCover.Cars.Api.Middleware;

/// <summary>
/// 
/// </summary>
public partial class ApiKeyMiddleware
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    public const string HEADER_KEY_NAME = "X-XSRF-TOKEN";

    /// <summary>
    /// 
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    /// 
    /// </summary>
    private readonly IWebHostEnvironment? _environment;

    /// <summary>
    /// 
    /// </summary>
    private const string AUTHORIZATION_HEADER_NAME = "Authorization";

    /// <summary>
    /// 
    /// </summary>
    private const string AUTHENTICATION_HEADER_NAME = "Authentication";

    /// <summary>
    /// 
    /// </summary>
    private const string AUTHORIZATION_BEARER_NAME = "Bearer";

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    public ApiKeyMiddleware(RequestDelegate next, IWebHostEnvironment? env) : base()
    {
        _environment = env;

        _next = next;
    }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext? context)
    {
        if (context != null)
        {
            if (context.Request.Headers.ContainsKey(HEADER_KEY_NAME) &&
                context.Request.Headers.TryGetValue(HEADER_KEY_NAME, out var tokens) && (tokens.Count > 0))
            {
                await AuthenticateTokenAsync(context, tokens);
            }
            else if (context.Request.Headers.ContainsKey(AUTHORIZATION_HEADER_NAME) &&
                    context.Request.Headers.Authorization.Any(x => x?.StartsWith(AUTHORIZATION_BEARER_NAME) ?? false))
            {
                await AuthorizeBearerTokenAsync(context, context.Request.Headers.Authorization);
            }
            else if (context.Request.Headers.ContainsKey(AUTHENTICATION_HEADER_NAME)) 
            {
                await AuthenticateBearerTokenAsync(context);
            }
            else
            {
                if (!context.Request.Headers.ContainsKey(HEADER_KEY_NAME) && !context.Session.IsAvailable)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                    await context.Response.WriteAsync($"Status Code: {context.Response.StatusCode}. The API request was not authorized to be processed.");
                }
                else if ((context.Request.Headers[HEADER_KEY_NAME].Count == 0) && !context.Session.IsAvailable)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.FailedDependency; // 424
                    await context.Response.WriteAsync($"Status Code: {context.Response.StatusCode}. The API request was not authorized to be processed.");
                }
                else if (context.Session.IsAvailable)
                {
                    // development use only
                    if (!context.Request.IsHttps && (_environment != null) && _environment.IsDevelopment())
                    {
                        await _next(context);
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync($"Status Code: {context.Response.StatusCode}. The API request was not authorized to be processed.");
                }
            }
        }

        await Task.Yield();
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private async Task AuthenticateBearerTokenAsync(HttpContext context)
    {
        var settings = context.RequestServices.GetRequiredService<IContextConfiguration>();

        if ((context != null) && context.Request.Headers.TryGetValue(AUTHENTICATION_HEADER_NAME, out var values))
        {
            if ((values.Count == 1) && !string.IsNullOrEmpty(settings.ApiKey))
            {
                var secretKey = values.FirstOrDefault(x => !string.IsNullOrEmpty(x));

                if (!string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(settings.ApiKey))
                {
                    if (settings.ApiKey.Trim().Equals(secretKey, StringComparison.Ordinal))
                    {
                        await _next(context);
                    }
                    else if (secretKey.Contains(AUTHORIZATION_BEARER_NAME, StringComparison.Ordinal))
                    {
                        var pos = secretKey.IndexOf(AUTHORIZATION_BEARER_NAME);

                        if (pos >= 0 && secretKey.Length > pos + AUTHORIZATION_BEARER_NAME.Length + 1)
                        {
                            var paraphrase = secretKey.Substring(startIndex: pos + AUTHORIZATION_BEARER_NAME.Length + 1).Trim();

                            if (settings.ApiKey.Trim().Equals(paraphrase, StringComparison.Ordinal))
                            {
                                await _next(context);
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403
                            await context.Response.WriteAsync("Error Code: 403. Error: The API request was not authorized to be processed.");
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                        await context.Response.WriteAsync("Error Code: 401. Error: The API request was not authorized to be processed.");
                    }
                }
                else if (string.IsNullOrEmpty(secretKey))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403
                    await context.Response.WriteAsync("Error Code: 403. Error: The API request was not authorized to be processed.");
                }
            }
            else if ((values.Count > 1) && !string.IsNullOrEmpty(settings.ApiKey))
            {
                var authorised = false;

                foreach (var value in values)
                {
                    if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(settings.ApiKey) && !value.Contains(AUTHORIZATION_BEARER_NAME))
                    {
                        if (value.Trim().Equals(settings.ApiKey, StringComparison.Ordinal))
                        {
                            authorised = true;
                            break;
                        }
                    }
                    else if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(settings.ApiKey) && value.Contains(AUTHORIZATION_BEARER_NAME))
                    {
                        var pos = value.IndexOf(AUTHORIZATION_BEARER_NAME);

                        if (pos >= 0)
                        {
                            var secretKey = value.Substring(startIndex: pos + AUTHORIZATION_BEARER_NAME.Length + 1).Trim();

                            if (secretKey.Equals(settings.ApiKey, StringComparison.Ordinal))
                            {
                                authorised = true;
                                break;
                            }
                        }
                    }
                }

                if (authorised)
                {
                    await _next(context);
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403
                    await context.Response.WriteAsync("Error Code: 403. Error: The API request was not authorized to be processed.");
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403

                await context.Response.WriteAsync("Error Code: 403. Error: The API request was not authorized to be processed by the provided toekn.");
            }
        }
        else if (context != null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403

            await context.Response.WriteAsync("Error Code: 403. Error: The API request was not authorized to be processed by the provided toekn.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    private async Task AuthenticateTokenAsync(HttpContext context, StringValues? values)
    {
        if ((context != null) && values.HasValue && values.Value.Count > 0)
        {
            var settings = context.RequestServices.GetRequiredService<IContextConfiguration>();

            if ((settings != null) && (settings.ApiKey != null) && values.HasValue && (values.Value.Count == 1) &&
                settings.ApiKey.Equals(values.Value, StringComparison.Ordinal))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403

                await context.Response.WriteAsync("Error Code: 403. Error: The API request was not authorized to be processed by the provided toekn.");
            }
        }
        else if (context != null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403

            await context.Response.WriteAsync("Error Code: 403. Error: The API request was not authorized to be processed by the provided toekn.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    private async Task AuthorizeBearerTokenAsync(HttpContext context, StringValues? values)
    {
        if ((context != null) && values.HasValue && values.Value.Count > 0)
        {
            var settings = context.RequestServices.GetRequiredService<IContextConfiguration>();

            var secretKey = values.Value.FirstOrDefault(x => !string.IsNullOrEmpty(x) && (x?.StartsWith(AUTHORIZATION_BEARER_NAME) ?? false));

            if ((settings != null) && !string.IsNullOrEmpty(settings.ApiKey) && !string.IsNullOrEmpty(secretKey))
            {
                var pos = secretKey.IndexOf(AUTHORIZATION_BEARER_NAME);

                if (pos >= 0)
                {
                    var paraphrase = secretKey.Substring(startIndex: pos + AUTHORIZATION_BEARER_NAME.Length + 1).Trim();

                    if (!string.IsNullOrEmpty(paraphrase) && paraphrase.Trim().Equals(settings.ApiKey.Trim(), StringComparison.Ordinal))
                    {
                        await _next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.FailedDependency; // 424
                        await context.Response.WriteAsync("Error Code: 424. Error: The API request was not authorized to be processed.");
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.FailedDependency; // 424
                    await context.Response.WriteAsync("Error Code: 424. Error: The API request was not authorized to be processed.");
                }
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                await context.Response.WriteAsync("Error Code: 401. Error: The API request was not authorized to be processed.");
            }
        }
        else if (context != null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
            await context.Response.WriteAsync("Error Code: 401. Error: The API request was not authorized to be processed.");
        }
    }

    #endregion
}
