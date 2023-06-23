using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Otterly.API.DataObjects.Bingo;

namespace Otterly.API.ClientLib;

public class OtterlyAPIClient : APIClientBase
{
	private readonly APIClientConfig _config;

	public OtterlyAPIClient(HttpClient client, APIClientConfig config) : base(client)
	{
		_config = config;
	}


	public async Task<OtterlyAppsUserDTO> GetUserProfile()
	{
		return new OtterlyAppsUserDTO();
	}

	public async Task<List<BingoCardDTO>> GetCards(Guid userID)
	{
		return await Get<BaseRequest, List<BingoCardDTO>>(new BaseRequest(userID), $"{_config.BaseURL}/bingo/card");
	}

}