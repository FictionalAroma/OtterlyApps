using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.Database.ActivityData.Interfaces;

public interface IPlayerCardDataService : IMongoServiceBase<PlayerTicket>
{
	Task<PlayerTicket> CreatePlayerTicket(Guid playerTwitchID, string sessionId, IEnumerable<PlayerTicketItem> randomisedSlots);
}