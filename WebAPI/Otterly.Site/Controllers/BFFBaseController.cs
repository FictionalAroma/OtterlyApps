using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib;

namespace Otterly.Site.Controllers;
public class BFFBaseController : ControllerBase
{
	private readonly ITypedHttpClientFactory<OtterlyAPIClient> _httpClientFactory;
	private readonly HttpClient _baseClient;


	public BFFBaseController(ITypedHttpClientFactory<OtterlyAPIClient> httpClientFactory, HttpClient baseClient)
	{
		_httpClientFactory = httpClientFactory;
		this._baseClient = baseClient;
	}

	protected async Task<OtterlyAPIClient> GenerateClientAsync()
	{
		var client = _httpClientFactory.CreateClient(_baseClient);
		client.Authentication =
			new AuthenticationHeaderValue("Bearer", await HttpContext.GetTokenAsync("Auth0", "access_token"));
		return client;
	}
}