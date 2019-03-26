using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HTTP
{
    public static class Get
    {
        public static async Task<JsonDeserializeModel> GetStreamAsync(Uri Url, CancellationToken cancellationToken, TimeSpan httpTimeout)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url);
            request.Headers.Add("Authorization-Token", "{THE TOKEN}");
            
            using (var client = new HttpClient { Timeout = httpTimeout})
         
            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                {
                    return ClientHelpers.DeserializeJsonFromStream<JsonDeserializeModel>(stream);
                }

                var content = await ClientHelpers.StreamToStringAsync(stream);
                throw new Exception($"{ (int)response.StatusCode } { content}");
            }
        }

    }
}
