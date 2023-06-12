using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otterly.API.ClientLib.Bingo;
using Otterly.Database.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface ICardHandler
{
    Task<List<BingoCard>> GetCardsForUser(Guid userID);
    Task<GetCardDetailsResponse> GetCardDetail(int cardID);
}