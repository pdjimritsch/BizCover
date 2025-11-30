using System.Net.Http.Headers;
using System.Text.Json;

namespace BizCover.Cars.Network;

using Abstraction;
using Headers;

/// <summary>
/// 
/// </summary>
partial class HttpCommunication
{
    #region IHttpCommunication Members - Put

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> PutAsync<TResponse>(string? url, object? component, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Put, url);

        HttpContent? content = default;

        if (component != null)
        {
            content = IsBinary(component) ?
                new ByteArrayContent((byte[])Convert.ChangeType(component, typeof(byte[]))) :
                new StringContent(JsonSerializer.Serialize(component));
        }

        HttpResponseMessage? server_response = default;

        if (cancellationToken == null && content != null)
        {
            if (!IsBinary(component))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            server_response = await _client.PutAsync(url, content);
        }
        else if (cancellationToken != null && content != null)
        {
            if (!IsBinary(component))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            server_response = await _client.PutAsync(url, content, cancellationToken.Value);
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
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> PutAsync<TResponse>(string? url, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Put, url);

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
    /// <param name="url"></param>
    /// <param name="component"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> PutAsync<TResponse, T>(string? url, ISoapRequest<T> component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
        where T : class, new()
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Put, url);

        HttpContent? content = default;

        if (component != null)
        {
            content = IsBinary(component) ?
                new ByteArrayContent((byte[])Convert.ChangeType(component, typeof(byte[]))) :
                new StringContent(JsonSerializer.Serialize(component));
        }

        HttpResponseMessage? server_response = default;

        if (cancellationToken == null && content != null)
        {
            if (!IsBinary(component))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            }

            server_response = await _client.PutAsync(url, content);
        }
        else if (cancellationToken != null && content != null)
        {
            if (!IsBinary(component))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            }

            server_response = await _client.PutAsync(url, content, cancellationToken.Value);
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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> PutAsync<TResponse>(Uri? uri, object? component, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        HttpContent? content = default;

        if (component != null)
        {
            content = IsBinary(component) ?
                new ByteArrayContent((byte[])Convert.ChangeType(component, typeof(byte[]))) :
                new StringContent(JsonSerializer.Serialize(component));
        }

        HttpResponseMessage? server_response = default;

        if (cancellationToken == null && content != null)
        {
            if (!IsBinary(component))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            server_response = await _client.PutAsync(uri, content);
        }
        else if (cancellationToken != null && content != null)
        {
            if (!IsBinary(component))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            server_response = await _client.PutAsync(uri, content, cancellationToken.Value);
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
    public async Task<TResponse?> PutAsync<TResponse>(Uri? uri, object? component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Put, uri.AbsolutePath);

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
    public async Task<TResponse?> PutAsync<TResponse, T>(Uri? uri, ISoapRequest<T> component, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
        where T : class, new()
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Put, uri.AbsolutePath);

        HttpContent? content = default;

        if (component != null)
        {
            content = IsBinary(component) ?
                new ByteArrayContent((byte[])Convert.ChangeType(component, typeof(byte[]))) :
                new StringContent(JsonSerializer.Serialize(component));
        }

        HttpResponseMessage? server_response = default;

        if (cancellationToken == null && content != null)
        {
            if (!IsBinary(component))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            }

            server_response = await _client.PutAsync(uri.AbsolutePath, content);
        }
        else if (cancellationToken != null && content != null)
        {
            if (!IsBinary(component))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
            }

            server_response = await _client.PutAsync(uri.AbsolutePath, content, cancellationToken.Value);
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            response = JsonSerializer.Deserialize<TResponse>(await server_response.Content.ReadAsStringAsync());
        }

        return await Task.FromResult(response);
    }

    #endregion
}
