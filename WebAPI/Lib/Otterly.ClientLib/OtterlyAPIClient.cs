using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Otterly.API.ClientLib.Account;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.DataObjects.User;

namespace Otterly.API.ClientLib;

public class OtterlyAPIClient : FactoryAPIClientBase
{
	private readonly APIClientConfig _config;

	public OtterlyAPIClient(HttpClient client, APIClientConfig config) : base(client)
	{
		_config = config;
	}


	public async Task<OtterlyAppsUserDTO> GetUserProfile()
	{
		return await Get<OtterlyAppsUserDTO>($"{_config.BaseURL}/account");
	}

	public async Task<List<BingoCardDTO>> GetCards(Guid userID)
	{
		return await Get<BaseRequest, List<BingoCardDTO>>(new BaseRequest(userID), $"{_config.BaseURL}/bingo/card");
	}

	public async Task<GetCardDetailsResponse> UpdateCard(BingoCardDTO cardToUpdate, Guid userID)
	{
		return await Post<UpdateCardDetailsRequest, GetCardDetailsResponse>($"{_config.BaseURL}/bingo/card",
																			new UpdateCardDetailsRequest()
																			{
																				CardDetails = cardToUpdate,
																				UserID = userID
																			});
	}

    public async Task<GetCardDetailsResponse> AddCard(BingoCardDTO cardToUpdate, Guid userID)
    {
		return await Put<UpdateCardDetailsRequest, GetCardDetailsResponse>($"{_config.BaseURL}/bingo/card",
																			new UpdateCardDetailsRequest()
																			{
																				CardDetails = cardToUpdate,
																				UserID = userID
																			});
    }

	public async Task<bool> DeleteCard(BingoCardDTO cardToUpdate, Guid userID)
	{
		return await Delete<UpdateCardDetailsRequest, bool>($"{_config.BaseURL}/bingo/card",
																		   new UpdateCardDetailsRequest()
																		   {
																			   CardDetails = cardToUpdate,
																			   UserID = userID
																		   });
	}

	public async Task<OtterlyAppsUserDTO> CreateUser(CreateUserRequest newUser)
	{
		return await Post<CreateUserRequest, OtterlyAppsUserDTO>($"{_config.BaseURL}/account/create", newUser);
	}

	public async Task<BingoSessionDTO?> GetCurrentGameSession(Guid userID)
	{
		return await Get<BingoSessionDTO?>(($"{_config.BaseURL}/bingo/game/getSessionForUser?userid={userID}"));
	}
	public async Task<BingoSessionDTO?> CreateGameSession(Guid userID, int cardID)
	{
		return await Post<CreateSessionRequest, BingoSessionDTO?>(($"{_config.BaseURL}/bingo/game/startnew"), new CreateSessionRequest()
																	  {
																		  UserID = userID,
																		  CardID = cardID
																	  });
	}
	public async Task<BaseResponse?> EndSession(string sessionID)
	{
		return await Get<BaseResponse?>(($"{_config.BaseURL}/bingo/game/endSession?sessionID={sessionID}"));
	}

	public async Task<BaseResponse?> VerifyItem(VerifyItemRequest request)
	{
		return await Post<VerifyItemRequest, BaseResponse>($"{_config.BaseURL}/bingo/game/verifyItem", request);
	}

	public async Task<BingoSessionMetaDTO?> GetSessionMeta(Guid userid)
	{
		return await Get<BingoSessionMetaDTO>($"{_config.BaseURL}/bingo/game/getSessionMeta?userid={userid}");

	}
}