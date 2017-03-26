using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebApi.Client
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResult<T>> GetAsync<T>(this HttpClient client, string address)
        {
            try
            {
                using (HttpResponseMessage response = await client.GetAsync(address))
                {
                    HttpResult<T> result = new HttpResult<T>() { StatusCode = response.StatusCode };
                    if (response.Content != null)
                    {
                        result.Content = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result));
                    }
                    return result;
                }
            }
            catch (HttpRequestException ex)
            {
                return HttpResult<T>.Failure(ex.Message);
            }
        }

        public static async Task<HttpResult<TResponse>> PostAsJsonAsync<TRequest, TResponse>(this HttpClient client, string address, TRequest content)
        {
            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, address))
                {
                    request.Content = new StringContent(await Task.Factory.StartNew(()=>JsonConvert.SerializeObject(content)));
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        HttpResult<TResponse> result = new HttpResult<TResponse>() { StatusCode = response.StatusCode };
                        if (response.Content != null)
                        {
                            
                            result.Content = await Task.Factory.StartNew(()=>JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result));
                        }
                        return result;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return HttpResult<TResponse>.Failure(ex.Message);
            }
        }

        public static async Task<HttpResult> DeleteItemAsync(this HttpClient client, string address)
        {
            try
            {
                using (HttpResponseMessage response = await client.DeleteAsync(address))
                {
                    return new HttpResult() { StatusCode = response.StatusCode };
                }
            }
            catch (HttpRequestException ex)
            {
                return HttpResult.Failure(ex.Message);
            }
        }

        public static async Task<HttpResult<T>> PatchAsync<T>(this HttpClient client, string address, T patch, bool returnContent = false)
        {
            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("PATCH"), address))
                {
                    request.Content = new StringContent(Task.Factory.StartNew(()=>JsonConvert.SerializeObject(patch)).Result);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    request.Headers.Add("Prefer", returnContent ? "return-content" : "return-no-content");
                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        HttpResult<T> result = new HttpResult<T>() { StatusCode = response.StatusCode };
                        if (returnContent)
                        {
                            result.Content = await Task.Factory.StartNew(()=>JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result));
                        }
                        return result;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return HttpResult<T>.Failure(ex.Message);
            }
        }

        public static async Task<HttpResult<TResponse>> UploadFileAsync<TResponse>(this HttpClient client, string address, MultipartFormDataContent content)
        {
            try
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, address))
                {
                    request.Content = content;
                    //request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        HttpResult<TResponse> result = new HttpResult<TResponse>() { StatusCode = response.StatusCode };
                        if (response.Content != null)
                        {

                            result.Content = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result));
                        }
                        return result;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return HttpResult<TResponse>.Failure(ex.Message);
            }
        }
    }
}
