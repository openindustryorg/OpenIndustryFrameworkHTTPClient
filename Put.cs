using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP
{
    public static class Put
    {
        public static void PutStream(object content, Uri Url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, Url);
            request.Headers.Add("Authorization-Token", "{THE TOKEN}");

            var httpContent = ClientHelpers.CreateHttpContent(content);
            request.Content = httpContent;

            client.SendAsync(request);
        }

        public static async Task PutStreamAsync(object content, Uri Url, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Url);
            request.Headers.Add("Authorization-Token", "{THE TOKEN}");

            using (var client = new HttpClient())
            using (var httpContent = ClientHelpers.CreateHttpContent(content))
            {
                request.Content = httpContent;

                using (var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                }
            }
        }
        
    }
}
