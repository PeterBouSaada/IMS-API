using Client.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Client.Classes
{
    public class HttpRequestService<T> : IHttpRequestService<T>
    {
        private HttpClient httpClient { get; set; }
        private LocalStorageService localStorage { get; set; }

        public HttpRequestService(HttpClient client, LocalStorageService storage)
        {
            httpClient = client;
            localStorage = storage;
        }

        public async Task<HttpResponseMessage> DeleteRequest(string id, string url)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string token = localStorage.GetItemAsString("BearerToken");
            if (token == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            url += "/" + id;
            response = await httpClient.DeleteAsync(url);

            return response;
        }

        public async Task<HttpResponseMessage> GetRequest(T data, string url)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string token = localStorage.GetItemAsString("BearerToken");
            if (token == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await httpClient.GetAsync(url);

            return response;
        }

        public async Task<HttpResponseMessage> PostRequest(T data, string url)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string token = localStorage.GetItemAsString("BearerToken");
            if (token == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string request = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            response = await httpClient.PostAsync(url, content);

            return response;
        }

        public async Task<HttpStatusCode> tokenRequest(T data)
        {
            string request = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("https://localhost:5001/API/user/authenticate", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                await localStorage.SetItemAsync("BearerToken", await response.Content.ReadAsStringAsync());
                Console.WriteLine("we added the bearer token");
            }
            return response.StatusCode;
        }

        public async Task<HttpResponseMessage> PutRequest(T data, string url)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            string token = localStorage.GetItemAsString("BearerToken");
            if (token == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string request = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var content = new StringContent(request, Encoding.UTF8, "application/json");
            response = await httpClient.PutAsync(url, content);

            return response;
        }

    }
}
