using System.Text.Json;

namespace BizCover.Cars.Network;

using Headers;

/// <summary>
/// 
/// </summary>
partial class HttpCommunication
{
    #region IHttpCommunication Members - Trace

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> TraceAsync<TResponse>(string? url, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Trace, url);

        if (component != null)
        {
            clientRequest.Content = IsBinary(component) ?
                new ByteArrayContent((byte[])Convert.ChangeType(component, typeof(byte[]))) :
                new StringContent(JsonSerializer.Serialize(component));
        }

        SetRequestHeaders(_client, requestHeader);

        SetRequestHeaders(clientRequest, requestHeader);

        var option = HttpCompletionOption.ResponseContentRead |
            HttpCompletionOption.ResponseHeadersRead;

        HttpResponseMessage server_response;

        if (cancellationToken == null)
        {
            server_response = await _client.SendAsync(clientRequest, option);
        }
        else
        {
            server_response = await _client.SendAsync(clientRequest, option, cancellationToken.Value);
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(await server_response.Content.ReadAsStringAsync());
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> TraceAsync<TResponse>(Uri? uri, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Trace, uri.AbsolutePath);

        if (component != null)
        {
            clientRequest.Content = IsBinary(component) ?
                new ByteArrayContent((byte[])Convert.ChangeType(component, typeof(byte[]))) :
                new StringContent(JsonSerializer.Serialize(component));
        }

        SetRequestHeaders(_client, requestHeader);

        SetRequestHeaders(clientRequest, requestHeader);

        var option = HttpCompletionOption.ResponseContentRead |
            HttpCompletionOption.ResponseHeadersRead;

        HttpResponseMessage server_response;

        if (cancellationToken == null)
        {
            server_response = await _client.SendAsync(clientRequest, option);
        }
        else
        {
            server_response = await _client.SendAsync(clientRequest, option, cancellationToken.Value);
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(await server_response.Content.ReadAsStringAsync());
        }

        return await Task.FromResult(response);
    }

    #endregion
}
