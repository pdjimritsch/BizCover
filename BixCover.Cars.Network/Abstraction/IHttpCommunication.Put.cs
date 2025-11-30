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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> PutAsync<TResponse>(string? url, object? component, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> PutAsync<TResponse>(string? url, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> PutAsync<TResponse, T>(string? url, ISoapRequest<T> component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
        where T : class, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="component"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> PutAsync<TResponse>(Uri? uri, object? component, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> PutAsync<TResponse>(Uri? uri, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> PutAsync<TResponse, T>(Uri? uri, ISoapRequest<T> component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
        where T : class, new();

    #endregion
}
