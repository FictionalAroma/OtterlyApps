using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Http;
using Otterly.API.ClientLib;

namespace Otterly.Site.Controllers;
[ApiController]
public class APILinkController : ControllerBase
{
	private readonly ITypedHttpClientFactory<OtterlyAPIClient> _httpClientFactory;
	private readonly HttpClient _baseClient;


	public APILinkController(ITypedHttpClientFactory<OtterlyAPIClient> httpClientFactory, HttpClient baseClient)
	{
		_httpClientFactory = httpClientFactory;
		this._baseClient = baseClient;
	}

	public Guid UserID
	{
		get
		{
			var claim = HttpContext.User.FindFirst("/uuid");
			if (claim != null && Guid.TryParse(claim.Value, out var result))
			{
				return result;
			}

			return Guid.Empty;
		}
	}

	protected async Task<OtterlyAPIClient> GenerateClientAsync()
	{
		var client = _httpClientFactory.CreateClient(_baseClient);
		client.Authentication =
			new AuthenticationHeaderValue("Bearer", await HttpContext.GetTokenAsync("Auth0", "access_token"));
		return client;
	}
}