using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Otterly.API.DataObjects.Bingo;

namespace Otterly.API.ClientLib;

public class OtterlyAPIClient : APIClientBase
{
	private readonly APIClientConfig _config;

	public OtterlyAPIClient(HttpClient client, IConfiguration config) : base(client)
	{
		var section = config.GetSection("OtterlyAPIConfig");
		_config = new APIClientConfig()
				  {
					  BaseURL = section["BaseURL"],
					  AuthName = section["AuthName"]
				  };
	}

	public override void AddAuthentication(HttpRequestMessage message)
	{
	}


	public async Task<OtterlyAppsUserDTO> GetUserProfile()
	{
		return new OtterlyAppsUserDTO();
	}

	public async Task<List<BingoCardDTO>> GetCards()
	{
		return await base.Get<List<BingoCardDTO>>($"{_config.BaseURL}/bingo/card");
	}
}