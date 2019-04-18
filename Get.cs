using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP
{
    public static class Get<T>
    {
        public static async Task<T> GetStreamAsync(Uri Url, CancellationToken cancellationToken, TimeSpan httpTimeout, string httpToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url);
            request.Headers.Add("Authorization", httpToken);
            
            using (var client = new HttpClient { Timeout = httpTimeout})
         
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
