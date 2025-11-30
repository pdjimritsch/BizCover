using System.Net;

using Microsoft.AspNetCore.Antiforgery;

using BizCover.Cars.Api.Features;

namespace BizCover.Cars.Api.Middleware;

/// <summary>
/// 
/// </summary>
public partial class AntiforgeryMiddleware
{
    #region Constants

    /// <summary>
    /// 
    /// </summary>
    private readonly static string AntiForgeryKey = "__BizCover_Cars_Antiforgery";

    /// <summary>
    /// 
    /// </summary>
    private static readonly object VaultAntiforgery = new ();

    #endregion

    #region Members

    /// <summary>
    /// 
    /// </summary>
    private readonly RequestDelegate? _next;

    /// <summary>
    /// 
    /// </summary>
    private readonly IAntiforgery? _antiforgery;

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    public AntiforgeryMiddleware(IAntiforgery? antiforgery, RequestDelegate? next) : base()
    {
        _antiforgery = antiforgery;

        _next = next;
    }

    #endregion

    #region Functions

    #pragma warning disable VSTHRD200

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint is not null)
        {
            context.Items[AntiForgeryKey] = VaultAntiforgery;
        }

        if ((endpoint is not null) && 
            (endpoint.Metadata.GetMetadata<IAntiforgeryMetadata>()  is {  RequiresValidation: true }))
        {
            InitiateVerificationAsync(context).ConfigureAwait(true).GetAwaiter().OnCompleted(() => { });
        }

        if (_next is not null)
        {
            return _next(context);
        }

        return Task.CompletedTask;
    }

    #pragma warning restore VSTHRD200

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InitiateVerificationAsync(HttpContext context)
    {
        try
        {
            if (_antiforgery is not null)
            {
                await _antiforgery.ValidateRequestAsync(context);
            }

            context.Features.Set(AntiforgeryVerifyFeature.Valid);
        }
        catch (AntiforgeryValidationException violation)
        {
            context.Features.Set<IAntiforgeryValidationFeature>(new AntiforgeryVerifyFeature(false, violation));

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
        }

        if (_next is not null)
        {
            await _next(context);
        }
        else
        {
            await Task.CompletedTask.ConfigureAwait(true);
        }

    }

    #endregion

}
