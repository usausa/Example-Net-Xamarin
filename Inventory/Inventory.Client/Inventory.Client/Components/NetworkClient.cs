namespace Inventory.Client.Components
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    /// <summary>
    ///
    /// </summary>
    public class NetworkClient : INetworkClient
    {
        private readonly NetworkClientOption option;

        public NetworkClient(NetworkClientOption option)
        {
            this.option = option;
        }

        public async Task<NetworkResult<T>> Get<T>(string path, TimeSpan? timeout, CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                if (timeout.HasValue)
                {
                    client.Timeout = timeout.Value;
                }

                try
                {
                    var response = await client.GetAsync(path, token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return new NetworkResult<T>(false, WebExceptionStatus.Success, response.StatusCode, default);
                    }

                    var json = await response.Content.ReadAsStringAsync();

                    var obj = JsonConvert.DeserializeObject<T>(json, option.SerializerSettings);

                    return new NetworkResult<T>(true, WebExceptionStatus.Success, response.StatusCode, obj);
                }
                catch (HttpRequestException)
                {
                    return new NetworkResult<T>(false, WebExceptionStatus.ConnectFailure, 0, default);
                }
                catch (WebException ex)
                {
                    return new NetworkResult<T>(false, ex.Status, (ex.Response as HttpWebResponse)?.StatusCode ?? 0, default);
                }
                catch (TaskCanceledException)
                {
                    return new NetworkResult<T>(false, WebExceptionStatus.Success, HttpStatusCode.RequestTimeout, default);
                }
                catch (IOException)
                {
                    return new NetworkResult<T>(false, WebExceptionStatus.UnknownError, 0, default);
                }
            }
        }

        public async Task<NetworkResult> Post<T>(string path, T request, TimeSpan? timeout, CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if (timeout.HasValue)
                    {
                        client.Timeout = timeout.Value;
                    }

                    var json = JsonConvert.SerializeObject(request, option.SerializerSettings);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(path, content, token);

                    return new NetworkResult(response.IsSuccessStatusCode, WebExceptionStatus.Success, response.StatusCode);
                }
                catch (HttpRequestException)
                {
                    return new NetworkResult(false, WebExceptionStatus.ConnectFailure, 0);
                }
                catch (WebException ex)
                {
                    return new NetworkResult(false, ex.Status, (ex.Response as HttpWebResponse)?.StatusCode ?? 0);
                }
                catch (TaskCanceledException)
                {
                    return new NetworkResult(false, WebExceptionStatus.Success, HttpStatusCode.RequestTimeout);
                }
                catch (IOException)
                {
                    return new NetworkResult<T>(false, WebExceptionStatus.UnknownError, 0, default);
                }
            }
        }

        public async Task<NetworkResult> Download(string path, Stream stream, TimeSpan? timeout, CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if (timeout.HasValue)
                    {
                        client.Timeout = timeout.Value;
                    }

                    var response = await client.GetAsync(path, token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return new NetworkResult(false, WebExceptionStatus.Success, response.StatusCode);
                    }

                    using (var input = await response.Content.ReadAsStreamAsync())
                    {
                        await input.CopyToAsync(stream);
                        await stream.FlushAsync(token);
                    }

                    return new NetworkResult(response.IsSuccessStatusCode, WebExceptionStatus.Success, response.StatusCode);
                }
                catch (HttpRequestException)
                {
                    return new NetworkResult(false, WebExceptionStatus.ConnectFailure, 0);
                }
                catch (WebException ex)
                {
                    return new NetworkResult(false, ex.Status, (ex.Response as HttpWebResponse)?.StatusCode ?? 0);
                }
                catch (TaskCanceledException)
                {
                    return new NetworkResult(false, WebExceptionStatus.Success, HttpStatusCode.RequestTimeout);
                }
                catch (IOException)
                {
                    return new NetworkResult(false, WebExceptionStatus.UnknownError, 0);
                }
            }
        }
    }
}
