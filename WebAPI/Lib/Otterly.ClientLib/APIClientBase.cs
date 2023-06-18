using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Interview.APIClients.Clients
{
    public abstract class APIClientBase
	{
		private HttpClient _client;

		public APIClientBase(HttpClient client)
		{
			_client = client;
		}

        public async Task<TOut> Get<TOut>(string url)
        {
            return await ProcessRequest<TOut>(new HttpRequestMessage(HttpMethod.Get, url));
        }

        public async Task<TOut> ProcessRequest<TOut>(HttpRequestMessage httpPayload)
        {
            httpPayload.Headers.Add("Accept", "*/*");
            httpPayload.Headers.Add("Accept-Encoding", "gzip, deflate, br");

            var responseObject = await _client.SendAsync(httpPayload);
            if (responseObject.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TOut>(await responseObject.Content.ReadAsStringAsync());
            }
            else
            {
                var responceData = await responseObject.Content.ReadAsStringAsync();
                throw new HttpRequestException(responceData);
            }
        }

        public async Task<TOut> Post<TRequest, TOut>(string url, TRequest request)
        {
            var http = new HttpRequestMessage(HttpMethod.Post, url);
            http.Content = new StringContent(JsonConvert.SerializeObject(request));
            return await Post<TOut>(http);
        }

        public async Task<TOut> Post<TOut>(HttpRequestMessage httpPayload)
        {
            return await ProcessRequest<TOut>(httpPayload);
        }

    }
}
