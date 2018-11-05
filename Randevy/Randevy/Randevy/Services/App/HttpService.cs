using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Randevy.Infrastructure.Constants;
using Randevy.Infrastructure.Extensions;
using Randevy.Infrastructure.Extensions.App;
using Randevy.Infrastructure.Interfaces.App;
using Randevy.Models.App;

namespace Randevy.Services.App
{
    public class HttpService : IHttpService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IJsonService _jsonService;

        public HttpService(
            ILocalStorageService localStorageService,
            IJsonService jsonService)
        {
            _localStorageService = localStorageService;
            _jsonService = jsonService;
        }


        public async Task<HttpResult<T>> GetAsync<T>(string resource, FilterBase filter, bool withAuth = true, Dictionary<string, string> additioalHeaders = null)
        {
            using (var client = HttpClient(SetHeaders(withAuth, additioalHeaders)))
            {
                using (var response = await client.GetAsync(RequestUrl(resource, filter)).ConfigureAwait(false))
                {
                    return await RetrieveContentAsync<T>(response);
                }
            }
        }


        public async Task<HttpResult> PostAsync(string resource, object requestBody, bool withAuth, Dictionary<string, string> additioalHeaders = null)
        {
            using (var client = HttpClient(SetHeaders(withAuth, additioalHeaders)))
            {
                var content = _jsonService.SerializeContent(requestBody);
                using (var response = await client.PostAsync(RequestUrl(resource), content).ConfigureAwait(false))
                {
                    return RetrieveAsync(response);
                }
            }
        }

        public async Task<HttpResult<T>> PostAsync<T>(string resource, object requestBody, bool withAuth, Dictionary<string, string> additioalHeaders = null)
        {
            using (var client = HttpClient(SetHeaders(withAuth, additioalHeaders)))
            {
                var content = _jsonService.SerializeContent(requestBody);
                using (var response = await client.PostAsync(RequestUrl(resource), content).ConfigureAwait(false))
                {
                    return await RetrieveContentAsync<T>(response);
                }
            }
        }

        public async Task<HttpResult> PutAsync(string resource, object requestBody, bool withAuth, Dictionary<string, string> additioalHeaders = null)
        {
            using (var client = HttpClient(SetHeaders(withAuth, additioalHeaders)))
            {
                var content = _jsonService.SerializeContent(requestBody);
                using (var response = await client.PutAsync(RequestUrl(resource), content).ConfigureAwait(false))
                {
                    return RetrieveAsync(response);
                }
            }
        }

        public async Task<HttpResult> DeleteAsync(string resource, bool withAuth, Dictionary<string, string> additioalHeaders = null)
        {
            using (var client = HttpClient(SetHeaders(withAuth, additioalHeaders)))
            {
                using (var response = await client.DeleteAsync(RequestUrl(resource)).ConfigureAwait(false))
                {
                    return RetrieveAsync(response);
                }
            }
        }

        public async Task<HttpResult> PatchAsync(string resource, object requestBody, bool withAuth, Dictionary<string, string> additioalHeaders = null)
        {
            using (var client = HttpClient(SetHeaders(withAuth, additioalHeaders)))
            {
                var content = _jsonService.SerializeContent(requestBody);
                using (var response = await client.PatchAsync(RequestUrl(resource), content).ConfigureAwait(false))
                {
                    return RetrieveAsync(response);
                }
            }
        }


        private HttpClient HttpClient(Dictionary<string, string> headerParams = null)
        {
            var httpClient = new HttpClient();
            if (headerParams == null) return httpClient;

            foreach (var headerParam in headerParams)
            {
                httpClient.DefaultRequestHeaders.Add(headerParam.Key, headerParam.Value);
            }

            return httpClient;
        }

        private string RequestUrl(string resource, FilterBase filter = null, string host = Constants.BaseUrl)
        {
            var parameters = BuildParametersString(filter);
            return $"{host}{resource}{parameters}";
        }

        private string BuildParametersString(FilterBase filter)
        {
            if (filter == null) return string.Empty;
        
            var sb = new StringBuilder();

            if (filter.Where.IsNotNullOrEmpty())
            {
                sb.SetUrlDivider().Append($"{nameof(filter.Where)}={WebUtility.UrlEncode(filter.Where)}");
            }

            if (filter.Search.IsNotNullOrEmpty())
                sb.SetUrlDivider().Append($"{nameof(filter.Search)}={WebUtility.UrlEncode(filter.Search)}");

            if (filter.Select.IsNotNullOrEmpty())
                sb.SetUrlDivider().Append($"{nameof(filter.Select)}={WebUtility.UrlEncode(filter.Select)}");

            if (filter.OrderBy.IsNotNullOrEmpty())
                sb.SetUrlDivider().Append($"{nameof(filter.OrderBy)}={WebUtility.UrlEncode(filter.OrderBy)}");

            if (filter.Take != null)
                sb.SetUrlDivider().Append($"{nameof(filter.Take)}={filter.Take}");

            if (filter.Skip != null)
                sb.SetUrlDivider().Append($"{nameof(filter.Skip)}={filter.Skip}");

            return sb.ToString();
        }
        
        private Dictionary<string, string> SetDefaultHeaders()
        {
            return new Dictionary<string, string>
            {
                ["User-Agent"] = "Mobile",
                ["Accept"] = "application/json"
            };
        }

        private Dictionary<string, string> SetHeaders(bool withAuth = false, Dictionary<string, string> additioalHeaders = null)
        {
            var headers = SetDefaultHeaders();
            if (withAuth) headers = SetAutorizationHeaders(headers);
            return SetAdditionalHeaders(headers, additioalHeaders);
        }

        private Dictionary<string, string> SetAutorizationHeaders(Dictionary<string, string> headers)
        {
            var token = _localStorageService.Load<string>(Constants.StorageKeys.Token);

            if (token.IsNotNullOrEmpty())
                headers.Add("Authorization", $"Bearer {token}");

            return headers;
        }

        private Dictionary<string, string> SetAdditionalHeaders(
            Dictionary<string, string> headers,
            Dictionary<string, string> additioalHeaders)
        {
            if (additioalHeaders == null) return headers;

            foreach (var kv in additioalHeaders)
            {
                headers[kv.Key] = kv.Value;
            }

            return headers;
        }

        private async Task<HttpResult<T>> RetrieveContentAsync<T>(HttpResponseMessage response)
        {
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                return new HttpResult<T>()
                {
                    HttpStatusCode = response.StatusCode,
                    Data = _jsonService.DeserializeStream<T>(json)
                };
            }
        }

        private HttpResult RetrieveAsync(HttpResponseMessage response)
        {
            return new HttpResult
            {
                HttpStatusCode = response.StatusCode
            };
        }
    }
}
