using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otterly.ClientLib.Bingo.Messaging;
using Otterly.Database.DataObjects;

namespace Otterly.API.Handlers.Interfaces;

public interface ICardHandler
{
    Task<List<BingoCard>> GetCardsForUser(Guid userID);
    Task<GetCardDetails> GetCardDetail(int cardID);
}