using System.ComponentModel;
using System.Security.Claims;
using System.Security.Principal;

namespace BizCover.Cars.Network.Communication;

using Abstraction;

/// <summary>
/// 
/// </summary>
[ImmutableObject(false)]
public sealed partial class TriggerResponse : IResponseContent
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public dynamic? Content { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ClaimsPrincipal? ClaimsPrincipal { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public static TriggerResponse Default => new TriggerResponse
    {
        ClaimsPrincipal = null,
        Content = null,
        Error = null,
        Method = HttpMethod.Trace,
        RequestMessage = null,
        ResponseMessage = null,
        Success = false,
        Token = null,
        User = null,
    };

    /// <summary>
    /// 
    /// </summary>
    public Exception? Error { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public HttpMethod? Method { get; set; } = HttpMethod.Get;

    /// <summary>
    /// 
    /// </summary>
    public HttpRequestMessage? RequestMessage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public HttpResponseMessage? ResponseMessage { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public bool? Success { get; set; }

    /// <summary>
    /// Json Web Token
    /// </summary>
    public string? Token { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IPrincipal? User { get; set; }

    #endregion
}
