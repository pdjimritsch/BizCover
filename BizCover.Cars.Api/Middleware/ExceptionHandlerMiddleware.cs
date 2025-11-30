using Microsoft;
using System.Text;

namespace BizCover.Cars.Api.Middleware;

/// <summary>
/// Application based middleware
/// </summary>
public partial class ExceptionHandlerMiddleware
{
    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly RequestDelegate? _next;

    /// <summary>
    /// 
    /// </summary>
    private readonly ILogger? _logger;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    public ExceptionHandlerMiddleware(RequestDelegate? next, ILoggerFactory? loggerFactory) : base()
    {
        _next = next;
        _logger = loggerFactory?.CreateLogger<ExceptionHandlerMiddleware>();

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
        try
        {
            if (_next != null && context != null)
            {
                if (context.Session.IsAvailable)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized access to the requested service");
                }
            }
        }
        catch (Exception violation) 
        {
            HandleErrors(context, violation, _logger);
        }

        await Task.Yield();
    }

    #endregion

    #region Internal Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="violation"></param>
    /// <param name="logger"></param>
    private static void HandleError(HttpContext? context, Exception? violation, ILogger? logger)
    {
        var builder = new StringBuilder();

        builder.AppendLine($"DateTime: {DateTime.UtcNow}");

        if (context != null) 
        {
            builder.AppendLine($"StatusCode: {context.Response.StatusCode}. Request: {context.Request.Method}. Destination: {context.Request.Path.Value ?? string.Empty}");
        }

        if (violation != null) 
        {
            builder.AppendLine($"Error: {violation.Message}");

            builder.AppendLine($"StackTrace: {violation.StackTrace}");
        }

        if (logger != null)
        {
            logger.LogError(builder.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="violation"></param>
    /// <param name="logger"></param>
    private static void HandleErrors(HttpContext? context, Exception? violation, ILogger? logger)
    {
        HandleError(context, violation, logger);

        if (violation?.InnerException != null)
        {
            HandleErrors(context, violation.InnerException, logger);
        }
    }

    #endregion
}
