using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Formatting;

namespace eShop.Web.Infrastructure
{
    public abstract class HttpClientBase
    {
        private readonly HttpClient _httpClient;
        public Uri _baseAddress;
        public string _basePath;
        public HttpClientBase()
        {
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

        public virtual async Task<T> SendRequest<T>(HttpRequestMessage request)
            where T : class
        {
            try
            {
                var httpClient = GetHttpClient();
                var response = await httpClient.SendAsync(request);
                T result = default(T);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<T>(GetFormatters());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<bool> SendRequest(HttpRequestMessage request)
        {
            try
            {
                var httpClient = GetHttpClient();
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected virtual IEnumerable<MediaTypeFormatter> GetFormatters()
        {
            // Make default the JSON
            return new List<MediaTypeFormatter>
            {
                new JsonMediaTypeFormatter()
            };
        }
    }
}