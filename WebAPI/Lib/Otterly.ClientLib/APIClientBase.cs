using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Otterly.API.ClientLib
{
    public abstract class FactoryAPIClientBase
	{
		private readonly HttpClient _client;
		public AuthenticationHeaderValue Authentication { get; set; }

		public FactoryAPIClientBase(HttpClient client)
		{
			_client = client;
		}

		public virtual Task<TOut> Get<TOut>(string url)
		{
			return ProcessRequest<TOut>(new HttpRequestMessage(HttpMethod.Get, url));
		}
		public virtual Task<TOut> Get<TRequest, TOut> (TRequest request, string url)
		{
			var http = new HttpRequestMessage(HttpMethod.Get, url);
			http.Content = new StringContent(JsonConvert.SerializeObject(request));
			return ProcessRequest<TOut>(http);
		}

		public virtual async Task<string> ProcessRequest(HttpRequestMessage httpPayload)
		{
			httpPayload.Headers.Add("Accept", "*/*");
			httpPayload.Headers.Add("Accept-Encoding", "gzip, deflate, br");
			if (httpPayload.Headers.Authorization == null)
			{
				httpPayload.Headers.Authorization = Authentication;
			}
			if (httpPayload.Content != null)
			{
				httpPayload.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			}

			var responseObject = await _client.SendAsync(httpPayload);
			if (responseObject.IsSuccessStatusCode)
			{
				return await responseObject.Content.ReadAsStringAsync();
			}

			var responceData = await responseObject.Content.ReadAsStringAsync();
			throw new HttpRequestException(responceData);
		}


        public virtual async Task<TOut> ProcessRequest<TOut>(HttpRequestMessage httpPayload)
		{
			var payload = await ProcessRequest(httpPayload);
			return !string.IsNullOrEmpty(payload) ? JsonConvert.DeserializeObject<TOut>(payload) : default;

		}

		public virtual Task Post<TRequest>(string url, TRequest request)
		{
			var http = new HttpRequestMessage(HttpMethod.Post, url)
					   {
						   Content = new StringContent(JsonConvert.SerializeObject(request))
					   };
			return ProcessRequest(http);
		}


        public virtual Task<TOut> Post<TRequest, TOut>(string url, TRequest request)
        {
            var http = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(request))
            };
            return ProcessRequest<TOut>(http);
        }
		public virtual Task<TOut> Put<TRequest, TOut>(string url, TRequest request)
		{
            var http = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(request))
            };
            return ProcessRequest<TOut>(http);
		}

		public virtual Task<TOut> Delete<TRequest, TOut>(string url, TRequest request)
		{
			var http = new HttpRequestMessage(HttpMethod.Delete, url)
					   {
						   Content = new StringContent(JsonConvert.SerializeObject(request))
					   };
			return ProcessRequest<TOut>(http);
		}

	}
}
