using System.Net.Http.Headers;

namespace BizCover.Cars.Network.Headers;

/// <summary>
/// 
/// </summary>
public sealed partial class ApiRequestHeader
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public HttpRequestHeaders? Headers { get; set; }

    #endregion
}
