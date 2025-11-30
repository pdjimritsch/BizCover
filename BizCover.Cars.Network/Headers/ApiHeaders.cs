using System.Net.Http.Headers;

namespace BizCover.Cars.Network.Headers;

/// <summary>
/// 
/// </summary>
/// 
public partial class ApiHeaders
{
    #region Properties

    /// <summary>
    /// Gets the value of the Accept header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<MediaTypeWithQualityHeaderValue>? Accept { get; }

    /// <summary>
    /// Gets the value of the Accept-Charset header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<StringWithQualityHeaderValue>? AcceptCharset { get; }

    /// <summary>
    /// Gets the value of the Accept-Encoding header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<StringWithQualityHeaderValue>? AcceptEncoding { get; }

    /// <summary>
    /// Gets the value of the Accept-Language header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<StringWithQualityHeaderValue>? AcceptLanguage { get; }

    /// <summary>
    /// Gets the value of the Accept-Ranges header for an HTTP response => ApiResponseHeader
    /// </summary>
    public HttpHeaderValueCollection<string>? AcceptRanges { get; }

    /// <summary>
    /// Gets or sets the value of the Age header for 
    /// an HTTP response => ApiResponseHeader.
    /// </summary>
    public TimeSpan? Age { get; set; }

    /// <summary>
    /// Gets or sets the value of the Authorization header for 
    /// an HTTP request => ApiRequestHeader.
    /// </summary>
    public AuthenticationHeaderValue? Authorization { get; set; }

    /// <summary>
    /// Gets or sets the value of the Cache-Control header for an HTTP request or 
    /// an HTTP response => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public CacheControlHeaderValue? CacheControl { get; set; }

    /// <summary>
    /// Gets the value of the Connection header for an HTTP request or 
    /// an HTTP response => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<string>? Connection { get; }

    /// <summary>
    /// Gets or sets a value that indicates if the Connection header 
    /// for an HTTP request or an HTTP response that contains 
    /// an Close => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public bool? ConnectionClose { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates the appropriate content type for
    /// and HTTP request => ApiRequestHeader.
    /// </summary>
    public string? ContentType { get; set; }

    /// <summary>
    /// Gets or sets the value of the Date header for an HTTP request or 
    /// an HTTP response => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public DateTimeOffset? Date { get; set; }

    /// <summary>
    /// Gets or sets the value of the ETag header for 
    /// an HTTP response => ApiResponseHeader.
    /// </summary>
    public EntityTagHeaderValue? ETag { get; set; }

    /// <summary>
    /// Gets the value of the Expect header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<NameValueWithParametersHeaderValue>? Expect { get; }

    /// <summary>
    /// Gets or sets a value that indicates if the Expect header 
    /// for an HTTP request that contains Continue => ApiRequestHeader.
    /// </summary>
    public bool? ExpectContinue { get; set; }

    /// <summary>
    /// Gets or sets the value of the From header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public string? From { get; set; }

    /// <summary>
    /// Gets or sets the value of the Host header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// Gets the value of the If-Match header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<EntityTagHeaderValue>? IfMatch { get; }

    /// <summary>
    /// Gets or sets the value of the If-Modified-Since header for 
    /// an HTTP request => ApiRequestHeader.
    /// </summary>
    public DateTimeOffset? IfModifiedSince { get; set; }

    /// <summary>
    /// Gets the value of the If-None-Match header for 
    /// an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<EntityTagHeaderValue>? IfNoneMatch { get; }

    /// <summary>
    /// Gets or sets the value of the If-Unmodified-Since header for 
    /// an HTTP request => ApiRequestHeader.
    /// </summary>
    public DateTimeOffset? IfUnmodifiedSince { get; set; }

    /// <summary>
    /// Gets or sets the value of the If-Range header for 
    /// an HTTP request => ApiRequestHeader.
    /// </summary>
    public RangeConditionHeaderValue? IfRange { get; set; }

    /// <summary>
    /// Gets or sets the value of the Location header for 
    /// an HTTP response => ApiResponseHeader.
    /// </summary>
    public Uri? Location { get; set; }

    /// <summary>
    /// Gets or sets the value of the Max-Forwards header for 
    /// an HTTP request => ApiRequestHeader.
    /// </summary>
    public int? MaxForwards { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public HttpMethod? Method { get; set; }

    /// <summary>
    /// Gets the value of the Pragma header for an HTTP request or 
    /// an HTTP response => => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<NameValueHeaderValue>? Pragma { get; }

    /// <summary>
    /// Gets the value of the Proxy-Authenticate header for an HTTP response => ApiResponseHeader
    /// </summary>
    public HttpHeaderValueCollection<AuthenticationHeaderValue>? ProxyAuthenticate { get; }

    /// <summary>
    /// Gets or sets the value of the Proxy-Authorization header for an HTTP request ==> ApiRequestHeader.
    /// </summary>
    public AuthenticationHeaderValue? ProxyAuthorization { get; set; }

    /// <summary>
    /// Gets or sets the value of the Range header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public RangeHeaderValue? Range { get; set; }

    /// <summary>
    /// Gets or sets the value of the Referer header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public Uri? Referrer { get; set; }

    /// <summary>
    /// Gets or sets the value of the Retry-After header for an HTTP response => ApiResponseHeader
    /// </summary>
    public RetryConditionHeaderValue? RetryAfter { get; set; }

    /// <summary>
    /// Gets the value of the Server header for an HTTP response => ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<ProductInfoHeaderValue>? Server { get; }

    /// <summary>
    /// Gets the value of the TE header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<TransferCodingWithQualityHeaderValue>? TE { get; }

    /// <summary>
    /// Gets the value of the Trailer header for an HTTP request 
    /// or an HTTP response => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<string>? Trailer { get; }

    /// <summary>
    /// Gets the value of the Transfer-Encoding header for an HTTP request 
    /// or and HTTP response => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<TransferCodingHeaderValue>? TransferEncoding { get; }

    /// <summary>
    /// Gets or sets a value that indicates if the Transfer-Encoding header for 
    /// an HTTP request or an HTTP response contains chunked => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public bool? TransferEncodingChunked { get; set; }

    /// <summary>
    /// Gets the value of the Upgrade header for an HTTP request 
    /// or an HTTP response => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<ProductHeaderValue>? Upgrade { get; }

    /// <summary>
    /// Gets the value of the User-Agent header for an HTTP request => ApiRequestHeader.
    /// </summary>
    public HttpHeaderValueCollection<ProductInfoHeaderValue>? UserAgent { get; }

    /// <summary>
    /// Gets the value of the Via header for an HTTP request or 
    /// and HTTP response => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<ViaHeaderValue>? Via { get; }

    /// <summary>
    /// Gets the value of the Vary header for an HTTP response => ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<string>? Vary { get; }

    /// <summary>
    /// Gets the value of the Warning header for an HTTP request or
    /// and HTTP response => ApiRequestHeader or ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<WarningHeaderValue>? Warning { get; }

    /// <summary>
    /// Gets the value of the WWW-Authenticate header for an HTTP response => ApiResponseHeader.
    /// </summary>
    public HttpHeaderValueCollection<AuthenticationHeaderValue>? WwwAuthenticate { get; }

    #endregion
}