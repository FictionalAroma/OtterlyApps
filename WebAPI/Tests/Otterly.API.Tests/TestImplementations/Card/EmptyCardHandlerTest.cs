using Otterly.API.ClientLib.Objects.Bingo;
using Otterly.API.Handlers.Interfaces;

namespace Otterly.API.Tests.TestImplementations.Card;

public class EmptyCardHandlerTest : ICardHandler
{
	public Task<List<BingoCardDTO>> GetCardsForUser(Guid userID)=> Task.FromResult(new List<BingoCardDTO>()); 

	public Task<BingoCardDTO?> GetCardDetail(int cardID, Guid requestUserID) => Task.FromResult<BingoCardDTO?>(null);

	public Task<BingoCardDTO?> UpdateCardDetails(Guid requestUserID, BingoCardDTO card) => Task.FromResult<BingoCardDTO?>(null);
	public Task<BingoCardDTO?> AddNewCard(Guid requestUserID, BingoCardDTO card) => Task.FromResult<BingoCardDTO?>(null);

	public Task<bool> DeleteCard(Guid requestUserID, BingoCardDTO requestCardDetails) => Task.FromResult(false);

}