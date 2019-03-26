using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP
{
    public static class Post
    {
        public static void PostStream(object content, Uri Url)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Headers.Add("Authorization-Token", "{THE TOKEN}");

            var httpContent = ClientHelpers.CreateHttpContent(content);
            request.Content = httpContent;

            client.SendAsync(request);
        }

        public static async Task PostStreamAsync(object content, Uri Url, CancellationToken cancellationToken)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, Url);
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
