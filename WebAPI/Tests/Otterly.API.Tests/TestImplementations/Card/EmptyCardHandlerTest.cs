using Otterly.API.ClientLib.Bingo;
using Otterly.API.Handlers.Interfaces;
using Otterly.Database.DataObjects;

namespace Otterly.API.Tests.TestImplementations.Card;

public class EmptyCardHandlerTest : ICardHandler
{
	public Task<List<BingoCard>> GetCardsForUser(Guid userID)
	{
		return Task.FromResult(new List<BingoCard>());
	}

	public Task<GetCardDetailsResponse> GetCardDetail(int cardID)
	{
		GetCardDetailsResponse? result = null;
		return Task.FromResult(result)!;
	}
}