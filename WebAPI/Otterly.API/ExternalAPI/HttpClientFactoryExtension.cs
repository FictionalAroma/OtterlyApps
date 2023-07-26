using System.Net.Http;
using Microsoft.Extensions.Http;

namespace Otterly.API.ExternalAPI;

public static class HttpClientFactoryExtension
{
	public static T GetClient<T>(this ITypedHttpClientFactory<T> fac)
	{
		var created = fac.CreateClient(new HttpClient());
		return created;
	}
}