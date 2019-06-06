using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP
{
    public static class Get<T>
    {
        public static async Task<T> GetJsonStreamAsync(Uri Url, string authorizationScheme, string authorizationToken, CancellationToken cancellationToken, TimeSpan httpTimeout)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url);

            using (var client = new HttpClient { Timeout = httpTimeout })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationScheme, authorizationToken);

                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return ClientHelpers.DeserializeJsonFromStream<T>(stream);
                    }

                    var content = await ClientHelpers.StreamToStringAsync(stream);
                    throw new Exception($"{ (int)response.StatusCode } { content}");
                }
            }
        }

    }
}
