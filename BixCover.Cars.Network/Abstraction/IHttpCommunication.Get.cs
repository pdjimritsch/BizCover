namespace BizCover.Cars.Network.Abstraction;

using Headers;

partial interface IHttpCommunication
{
    #region Asyncrhonous Functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="content_type"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> GetAsync<TResponse>(string? url, string? content_type = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> GetAsync<TResponse>(string? url, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content_type"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> GetAsync<TResponse>(Uri? uri, string? content_type = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> GetAsync<TResponse>(Uri? uri, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="content_type"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> GetAsync<T, TResponse>(string? url, string? content_type, Func<T, bool>? predicate = null, CancellationToken? cancellationToken = null)
        where T : class
        where TResponse : IResponseContent, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> GetAsync<T, TResponse>(string? url, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, Func<T, bool>? predicate = null, CancellationToken? cancellationToken = null)
        where T : class
        where TResponse : IResponseContent, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> GetAsync<T, TResponse>(Uri? url, string? content_type, Func<T, bool>? predicate = null, CancellationToken? cancellationToken = null)
        where T : class
        where TResponse : IResponseContent, new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TResponse?> GetAsync<T, TResponse>(Uri? url, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, Func<T, bool>? predicate = null, CancellationToken? cancellationToken = null)
        where T : class
        where TResponse : IResponseContent, new();

    #endregion
}
