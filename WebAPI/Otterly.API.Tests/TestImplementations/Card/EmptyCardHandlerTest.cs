using Otterly.API.Handlers.Interfaces;
using Otterly.ClientLib.Bingo.Messaging;
using Otterly.Database.DataObjects;

namespace Otterly.API.Tests.TestImplementations.Card;

public class EmptyCardHandlerTest : ICardHandler
{
	public Task<List<BingoCard>> GetCardsForUser(Guid userID)
	{
		return Task.FromResult(new List<BingoCard>());
	}

	public Task<GetCardDetails> GetCardDetail(int cardID)
	{
		GetCardDetails? result = null;
		return Task.FromResult(result)!;
	}
}