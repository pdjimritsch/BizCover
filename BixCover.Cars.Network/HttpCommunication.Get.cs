using System.Text.Json;

namespace BizCover.Cars.Network;

using Abstraction;
using Headers;
using System.Net.Http.Json;

/// <summary>
/// 
/// </summary>
partial class HttpCommunication
{
    #region IHttpCommunication Members - Asynchronouos Get

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="content_type"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> GetAsync<TResponse>(string? url, string? content_type = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var option = HttpCompletionOption.ResponseContentRead |
            HttpCompletionOption.ResponseHeadersRead;

        HttpResponseMessage? server_response = default;

        if (cancellationToken == null && string.IsNullOrEmpty(content_type))
        {
            server_response = await _client.GetAsync(url, option);
        }
        else if (cancellationToken == null && !string.IsNullOrEmpty(content_type))
        {
            if (content_type.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                var json_option = new JsonSerializerOptions { MaxDepth = int.MaxValue };

                response = await _client.GetFromJsonAsync<TResponse>(url, json_option);
            }
            else
            {
                server_response = await _client.GetAsync(url, option);
            }
        }
        else if (cancellationToken != null && string.IsNullOrEmpty(content_type))
        {
            server_response = await _client.GetAsync(url, option, cancellationToken.Value);
        }
        else if (cancellationToken != null && !string.IsNullOrEmpty(content_type))
        {
            if (content_type.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                var json_option = new JsonSerializerOptions { MaxDepth = int.MaxValue };

                response = await _client.GetFromJsonAsync<TResponse>(url, json_option, cancellationToken.Value);
            }
            else
            {
                server_response = await _client.GetAsync(url, option, cancellationToken.Value);
            }
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            var content = await server_response.Content.ReadAsStringAsync();

            if (ReferenceEquals(typeof(TResponse), content.GetType()))
            {
                response = (TResponse)Convert.ChangeType(content, typeof(TResponse));
            }
            else
            {
                response = JsonSerializer.Deserialize<TResponse>(content);
            }
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> GetAsync<TResponse>(string? url, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Get, url);

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
            var content = await server_response.Content.ReadAsStringAsync();

            if (ReferenceEquals(typeof(TResponse), content.GetType()))
            {
                response = (TResponse)Convert.ChangeType(content, typeof(TResponse));
            }
            else
            {
                response = JsonSerializer.Deserialize<TResponse>(content);
            }
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="content_type"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> GetAsync<TResponse>(Uri? uri, string? content_type = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        var option = HttpCompletionOption.ResponseContentRead |
            HttpCompletionOption.ResponseHeadersRead;

        HttpResponseMessage? server_response = default;

        if (cancellationToken == null && string.IsNullOrEmpty(content_type))
        {
            server_response = await _client.GetAsync(uri, option);
        }
        else if (cancellationToken == null && !string.IsNullOrEmpty(content_type))
        {
            if (content_type.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                var json_option = new JsonSerializerOptions { MaxDepth = int.MaxValue };

                response = await _client.GetFromJsonAsync<TResponse>(uri, json_option);
            }
            else
            {
                server_response = await _client.GetAsync(uri, option);
            }
        }
        else if (cancellationToken != null && string.IsNullOrEmpty(content_type))
        {
            server_response = await _client.GetAsync(uri, option, cancellationToken.Value);
        }
        else if (cancellationToken != null && !string.IsNullOrEmpty(content_type))
        {
            if (content_type.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                var json_option = new JsonSerializerOptions { MaxDepth = int.MaxValue };

                response = await _client.GetFromJsonAsync<TResponse>(uri, json_option, cancellationToken.Value);
            }
            else
            {
                server_response = await _client.GetAsync(uri, option, cancellationToken.Value);
            }
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            var content = await server_response.Content.ReadAsStringAsync();

            if (ReferenceEquals(typeof(TResponse), content.GetType()))
            {
                response = (TResponse)Convert.ChangeType(content, typeof(TResponse));
            }
            else
            {
                response = JsonSerializer.Deserialize<TResponse>(content);
            }
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> GetAsync<TResponse>(Uri? uri, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, CancellationToken? cancellationToken = null)
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Get, uri.AbsolutePath);

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
            var content = await server_response.Content.ReadAsStringAsync();

            if (ReferenceEquals(typeof(TResponse), content.GetType()))
            {
                response = (TResponse)Convert.ChangeType(content, typeof(TResponse));
            }
            else
            {
                response = JsonSerializer.Deserialize<TResponse>(content);
            }
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> GetAsync<T, TResponse>(string? url, string? content_type, Func<T, bool>? predicate = null, CancellationToken? cancellationToken = null)
        where T : class
        where TResponse : IResponseContent, new()
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var option = HttpCompletionOption.ResponseContentRead |
            HttpCompletionOption.ResponseHeadersRead;

        HttpResponseMessage? server_response = default;

        if (cancellationToken == null && string.IsNullOrEmpty(content_type))
        {
            server_response = await _client.GetAsync(url, option);
        }
        else if (cancellationToken == null && !string.IsNullOrEmpty(content_type))
        {
            if (content_type.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                response = await _client.GetFromJsonAsync<TResponse?>(url);
            }
            else
            {
                server_response = await _client.GetAsync(url, option);
            }
        }
        else if (cancellationToken != null && string.IsNullOrEmpty(content_type))
        {
            server_response = await _client.GetAsync(url, option, cancellationToken.Value);
        }
        else if (cancellationToken != null && !string.IsNullOrEmpty(content_type))
        {
            if (content_type.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                var json_option = new JsonSerializerOptions { MaxDepth = int.MaxValue };

                response = await _client.GetFromJsonAsync<TResponse>(url, json_option, cancellationToken.Value);
            }
            else
            {
                server_response = await _client.GetAsync(url, option, cancellationToken.Value);
            }
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            var content = JsonSerializer.Deserialize<T>(await server_response.Content.ReadAsStringAsync());

            try
            {
                if (content != null &&
                    content.GetType().IsAssignableTo(typeof(IEnumerable<T>)) &&
                    typeof(IEnumerable<T>).IsAssignableTo(typeof(TResponse)))
                {
                    var sequence = (IEnumerable<T>)Convert.ChangeType(content, typeof(IEnumerable<T>));

                    if (sequence?.Any() ?? false)
                    {
                        if (predicate != null)
                        {
                            response = (TResponse)Convert.ChangeType(sequence.Where(x => predicate(x)), typeof(TResponse));
                        }
                        else
                        {
                            response = (TResponse)Convert.ChangeType(sequence, typeof(TResponse));
                        }
                    }
                }
                else if (content != null && typeof(T).IsAssignableTo(typeof(TResponse)))
                {
                    response = (TResponse)Convert.ChangeType(content, typeof(TResponse));
                }
                else if (content != null)
                {
                    response = Activator.CreateInstance<TResponse>();
                    response.Content = content;
                }
            }
            catch (Exception violation)
            {
                AddError(violation);
            }
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> GetAsync<T, TResponse>(string? url, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, Func<T, bool>? predicate = null, CancellationToken? cancellationToken = null)
        where T : class
        where TResponse : IResponseContent, new()
    {
        TResponse? response = default;

        if (_client == null || string.IsNullOrEmpty(url))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Get, url);

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
            var content = JsonSerializer.Deserialize<T>(await server_response.Content.ReadAsStringAsync());

            try
            {
                if (content != null &&
                    content.GetType().IsAssignableTo(typeof(IEnumerable<T>)) &&
                    typeof(IEnumerable<T>).IsAssignableTo(typeof(TResponse)))
                {
                    var sequence = (IEnumerable<T>)Convert.ChangeType(content, typeof(IEnumerable<T>));

                    if (sequence?.Any() ?? false)
                    {
                        if (predicate != null)
                        {
                            response = (TResponse)Convert.ChangeType(sequence.Where(x => predicate(x)), typeof(TResponse));
                        }
                        else
                        {
                            response = (TResponse)Convert.ChangeType(sequence, typeof(TResponse));
                        }
                    }
                }
                else if (content != null && typeof(T).IsAssignableTo(typeof(TResponse)))
                {
                    response = (TResponse)Convert.ChangeType(content, typeof(TResponse));
                }
                else if (content != null)
                {
                    response = Activator.CreateInstance<TResponse>();
                    response.Content = content;
                }
            }
            catch (Exception violation)
            {
                AddError(violation);
            }
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> GetAsync<T, TResponse>(Uri? uri, string? content_type, Func<T, bool>? predicate = null, CancellationToken? cancellationToken = null)
        where T : class
        where TResponse : IResponseContent, new()
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        var option = HttpCompletionOption.ResponseContentRead |
            HttpCompletionOption.ResponseHeadersRead;

        HttpResponseMessage? server_response = default;

        if (cancellationToken == null && string.IsNullOrEmpty(content_type))
        {
            server_response = await _client.GetAsync(uri, option);
        }
        else if (cancellationToken == null && !string.IsNullOrEmpty(content_type))
        {
            if (content_type.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                var json_option = new JsonSerializerOptions { MaxDepth = int.MaxValue };

                response = await _client.GetFromJsonAsync<TResponse>(uri, json_option);
            }
            else
            {
                server_response = await _client.GetAsync(uri, option);
            }
        }
        else if (cancellationToken != null && string.IsNullOrEmpty(content_type))
        {
            server_response = await _client.GetAsync(uri, option, cancellationToken.Value);
        }
        else if (cancellationToken != null && !string.IsNullOrEmpty(content_type))
        {
            if (content_type.Equals("application/json", StringComparison.InvariantCultureIgnoreCase))
            {
                var json_option = new JsonSerializerOptions { MaxDepth = int.MaxValue };

                response = await _client.GetFromJsonAsync<TResponse>(uri, json_option, cancellationToken.Value);
            }
            else
            {
                server_response = await _client.GetAsync(uri, option, cancellationToken.Value);
            }
        }

        if (server_response != null && server_response.IsSuccessStatusCode && server_response.Content != null)
        {
            var content = JsonSerializer.Deserialize<T>(await server_response.Content.ReadAsStringAsync());

            try
            {
                if (content != null &&
                    content.GetType().IsAssignableTo(typeof(IEnumerable<T>)) &&
                    typeof(IEnumerable<T>).IsAssignableTo(typeof(TResponse)))
                {
                    var sequence = (IEnumerable<T>)Convert.ChangeType(content, typeof(IEnumerable<T>));

                    if (sequence?.Any() ?? false)
                    {
                        if (predicate != null)
                        {
                            response = (TResponse)Convert.ChangeType(sequence.Where(x => predicate(x)), typeof(TResponse));
                        }
                        else
                        {
                            response = (TResponse)Convert.ChangeType(sequence, typeof(TResponse));
                        }
                    }
                }
                else if (content != null && typeof(T).IsAssignableTo(typeof(TResponse)))
                {
                    response = (TResponse)Convert.ChangeType(content, typeof(TResponse));
                }
                else if (content != null)
                {
                    response = Activator.CreateInstance<TResponse>();
                    response.Content = content;
                }
            }
            catch (Exception violation)
            {
                AddError(violation);
            }
        }

        return await Task.FromResult(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="requestHeader"></param>
    /// <param name="responseHeader"></param>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<TResponse?> GetAsync<T, TResponse>(Uri? uri, ApiRequestHeader? requestHeader, ApiResponseHeader? responseHeader = null, Func<T, bool>? predicate = null, CancellationToken? cancellationToken = null)
        where T : class
        where TResponse : IResponseContent, new()
    {
        TResponse? response = default;

        if (_client == null || (uri == null) || string.IsNullOrEmpty(uri.AbsolutePath))
        {
            return await Task.FromResult(response);
        }

        var clientRequest = new HttpRequestMessage(HttpMethod.Get, uri.AbsolutePath);

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
            var content = JsonSerializer.Deserialize<T>(await server_response.Content.ReadAsStringAsync());

            try
            {
                if (content != null &&
                    content.GetType().IsAssignableTo(typeof(IEnumerable<T>)) &&
                    typeof(IEnumerable<T>).IsAssignableTo(typeof(TResponse)))
                {
                    var sequence = (IEnumerable<T>)Convert.ChangeType(content, typeof(IEnumerable<T>));

                    if (sequence?.Any() ?? false)
                    {
                        if (predicate != null)
                        {
                            response = (TResponse)Convert.ChangeType(sequence.Where(x => predicate(x)), typeof(TResponse));
                        }
                        else
                        {
                            response = (TResponse)Convert.ChangeType(sequence, typeof(TResponse));
                        }
                    }
                }
                else if (content != null && typeof(T).IsAssignableTo(typeof(TResponse)))
                {
                    response = (TResponse)Convert.ChangeType(content, typeof(TResponse));
                }
                else if (content != null)
                {
                    response = Activator.CreateInstance<TResponse>();
                    response.Content = content;
                }
            }
            catch (Exception violation)
            {
                AddError(violation);
            }
        }

        return await Task.FromResult(response);
    }

    #endregion
}
