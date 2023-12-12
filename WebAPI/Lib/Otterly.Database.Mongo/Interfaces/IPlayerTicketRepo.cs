using System.Collections.Generic;
using System.Threading.Tasks;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.Database.ActivityData.Interfaces;

public interface IPlayerTicketRepo
{
	Task<PlayerTicket> CreatePlayerTicket(string playerTwitchID,
										  string requestPlayerScreenName,
										  string sessionId,
										  IEnumerable<PlayerTicketItem> randomisedSlots);
	Task<PlayerTicket?> GetTicketByID(string cardID);
	Task<PlayerTicket?> FindTicket(string playerTwitchID, string sessionId);
	Task<List<PlayerTicket>> GetAllTicketsForSession(string markedItemSessionID);

	Task UpdateAsync(PlayerTicket ticket);
	Task UpdateListAsync(IEnumerable<PlayerTicket> tickets);
}