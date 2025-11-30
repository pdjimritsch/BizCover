namespace BizCover.Cars.Network.Abstraction;

using Headers;

partial interface IHttpCommunication
{
    #region Asynchronous Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> TraceAsync<TResponse>(string? url, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> TraceAsync<TResponse>(Uri? uri, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null);

    #endregion
}
