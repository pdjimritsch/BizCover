using System.Text.Json;

namespace BizCover.Cars.Network;

using Headers;

/// <summary>
/// 
/// </summary>
partial class HttpCommunication
{
    #region IHttpCommunication Members - Delete

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> DeleteAsync<TResponse>(string? url, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        HttpResponseMessage? server_response;

        if (cancellationToken == null)
        {
            server_response = await _client.DeleteAsync(url);
        }
        else
        {
            server_response = await _client.DeleteAsync(url, cancellationToken.Value);
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(await server_response.Content.ReadAsStringAsync());
        }

        return response;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> DeleteAsync<TResponse>(string? url, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Delete, url);

        if (component != null)
        {
            clientRequest.Content = IsBinary(component) ?
                new ByteArrayContent((byte[])Convert.ChangeType(component, typeof(byte[]))) :
                new StringContent(JsonSerializer.Serialize(component));
        }

        SetRequestHeaders(_client, requestHeader);

        SetRequestHeaders(clientRequest, requestHeader);

        HttpResponseMessage server_response;

        var option = HttpCompletionOption.ResponseContentRead |
            HttpCompletionOption.ResponseHeadersRead;

        if (cancellationToken == null)
        {
            server_response = await _client.SendAsync(clientRequest, option); ;
        }
        else
        {
            server_response = await _client.SendAsync(clientRequest, option, cancellationToken.Value);
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(await server_response.Content.ReadAsStringAsync());
        }

        return response;
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
    public async Task<TResponse?> DeleteAsync<TResponse>(Uri? uri, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        HttpResponseMessage server_response;

        if (cancellationToken == null)
        {
            server_response = await _client.DeleteAsync(uri);
        }
        else
        {
            server_response = await _client.DeleteAsync(uri, cancellationToken.Value);
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(await server_response.Content.ReadAsStringAsync());
        }

        return response;
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
    public async Task<TResponse?> DeleteAsync<TResponse>(Uri? uri, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Delete, uri.AbsolutePath);

        if (component != null)
        {
            clientRequest.Content = IsBinary(component) ?
                new ByteArrayContent((byte[])Convert.ChangeType(component, typeof(byte[]))) :
                new StringContent(JsonSerializer.Serialize(component));
        }

        SetRequestHeaders(_client, requestHeader);

        SetRequestHeaders(clientRequest, requestHeader);

        HttpResponseMessage server_response;

        var option = HttpCompletionOption.ResponseContentRead |
            HttpCompletionOption.ResponseHeadersRead;

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

        return response;
    }

    #endregion
}
