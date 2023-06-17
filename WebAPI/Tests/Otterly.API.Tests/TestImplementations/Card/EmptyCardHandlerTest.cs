using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.Handlers.Interfaces;
using Otterly.ClientLib;

namespace Otterly.API.Tests.TestImplementations.Card;

public class EmptyCardHandlerTest : ICardHandler
{
	public Task<List<BingoCardDTO>> GetCardsForUser(Guid userID)
	{
		return Task.FromResult(new List<BingoCardDTO>());
	}

	public Task<GetCardDetailsResponse> GetCardDetail(int cardID)
	{
		GetCardDetailsResponse? result = null;
		return Task.FromResult(result)!;
	}

	public Task<BaseResponse> UpdateCardDetails(UpdateCardDetailsRequest request) { throw new NotImplementedException(); }
}