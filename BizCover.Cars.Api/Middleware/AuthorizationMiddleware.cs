using System.Net;

using BizCover.Cars.Configuration.Abstraction;

using Microsoft.Extensions.Primitives;

namespace BizCover.Cars.Api.Middleware;

/// <summary>
/// 
/// </summary>
public class AuthorizationMiddleware
{
    #region Members

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
    private const string AUTHORIZATION_BEARER_NAME = "Bearer";

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    /// <param name="env"></param>
    public AuthorizationMiddleware(RequestDelegate next, IWebHostEnvironment? env) : base()
    {
        _next = next;

        _environment = env;
    }

    #endregion

    #region Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
         if (context.Request.Headers.ContainsKey(AUTHORIZATION_HEADER_NAME) &&
            context.Request.Headers.Authorization.Any(x => x?.StartsWith(AUTHORIZATION_BEARER_NAME) ?? false))
        {
            await AuthorizeBearerTokenAsync(context, context.Request.Headers.Authorization);
        }

        await Task.Yield();
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    private async Task AuthorizeBearerTokenAsync(HttpContext context, StringValues? values)
    {
        await Task.Run(async () =>
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
        });

        await Task.Yield();
    }

    #endregion

}