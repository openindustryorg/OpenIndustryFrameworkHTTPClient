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
    public static class Put<T>
    {
        public static void PutJsonStream(T content, Uri Url, string authorizationScheme, string authorizationToken)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, Url);
            var httpContent = ClientHelpers.CreateHttpJsonContent<T>(content);

            request.Content = httpContent;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationScheme, authorizationToken);

            client.SendAsync(request);
        }

        public static async Task PutStreamAsync(T content, Uri Url, string authorizationScheme, string authorizationToken, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, Url);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationScheme, authorizationToken);

                using (var httpContent = ClientHelpers.CreateHttpJsonContent<T>(content))
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
}