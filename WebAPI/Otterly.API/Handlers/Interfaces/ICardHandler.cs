using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otterly.API.ClientLib.Objects.Bingo;

namespace Otterly.API.Handlers.Interfaces;

public interface ICardHandler
{
    Task<List<BingoCardDTO>> GetCardsForUser(Guid userID);
    Task<BingoCardDTO?> GetCardDetail(int cardID, Guid requestUserID);
	Task<BingoCardDTO?> UpdateCardDetails(Guid requestUserID, BingoCardDTO card);
	Task<BingoCardDTO?> AddNewCard(Guid requestUserID, BingoCardDTO card);
	Task<bool> DeleteCard(Guid requestUserID, BingoCardDTO requestCardDetails);
}