using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP
{
    public static class Post<T>
    {
        public static void PostStream(T content, Uri Url, string httpToken)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Headers.Add("Authorization-Token", httpToken);

            var httpContent = ClientHelpers.CreateHttpContent<T>(content);
            request.Content = httpContent;

            client.SendAsync(request);
        }

        public static async Task PostStreamAsync(T content, Uri Url, CancellationToken cancellationToken, string httpToken)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Headers.Add("Authorization-Token", httpToken);

            using (var client = new HttpClient())
            using (var httpContent = ClientHelpers.CreateHttpContent<T>(content))
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
