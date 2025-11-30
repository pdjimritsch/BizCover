using System.Net.Http.Headers;

namespace BizCover.Cars.Network.Headers;

/// <summary>
/// 
/// </summary>
public sealed partial class ApiResponseHeader
{
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public HttpResponseHeaders? Headers { get; set; }

    #endregion
}
