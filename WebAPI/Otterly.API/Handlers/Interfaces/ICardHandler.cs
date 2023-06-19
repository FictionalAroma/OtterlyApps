using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otterly.API.ClientLib;
using Otterly.API.ClientLib.Bingo;
using Otterly.API.DataObjects.Bingo;

namespace Otterly.API.Handlers.Interfaces;

public interface ICardHandler
{
    Task<List<BingoCardDTO>> GetCardsForUser(Guid userID);
    Task<GetCardDetailsResponse> GetCardDetail(int cardID);
	Task<BaseResponse> UpdateCardDetails(UpdateCardDetailsRequest request);
}