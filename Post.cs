using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP
{
    public static class Post<T>
    {
        public static void PostJsonStream(T content, Uri Url, string authorizationScheme, string authorizationToken)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            var httpContent = ClientHelpers.CreateHttpJsonContent<T>(content);
         
            request.Content = httpContent;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationScheme, authorizationToken);

            client.SendAsync(request);
        }

        public static async Task PostJsonStreamAsync(T content, Uri Url, string authorizationScheme, string authorizationToken, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Url);

            using (var client = new HttpClient())
            using (var httpContent = ClientHelpers.CreateHttpJsonContent<T>(content))
            {
                request.Content = httpContent;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationScheme, authorizationToken);

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
