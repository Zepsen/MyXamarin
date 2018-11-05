using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Randevy.Infrastructure.Extensions.App
{
    public static class HttpClientExt
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUrl, HttpContent content)
        {
            var request = new HttpRequestMessage
            {
                Content = content,
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(requestUrl)
            };

            return await client.SendAsync(request);
        }
    }
}
