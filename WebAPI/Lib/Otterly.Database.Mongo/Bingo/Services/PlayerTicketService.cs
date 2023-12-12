using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Configuration;
using Otterly.Database.ActivityData.Interfaces;

namespace Otterly.Database.ActivityData.Bingo.Services;

public class PlayerTicketService : MongoServiceBase<PlayerTicket>, IPlayerTicketRepo
{
	public PlayerTicketService(MongoDBConfig config, MongoClient client) : base(config, client, "bingo.playercards")
	{


	}


	public async Task<PlayerTicket> CreatePlayerTicket(string playerTwitchID,
													   string requestPlayerScreenName,
													   string sessionId,
													   IEnumerable<PlayerTicketItem> randomisedSlots)
	{

		var newTicket = new PlayerTicket()
						{
							SessionID = sessionId,
							TwitchUserID = playerTwitchID,
							TwitchDisplayName = requestPlayerScreenName,
							Slots = randomisedSlots
						};
		await CreateAsync(newTicket);

		return (await FindTicket(playerTwitchID, sessionId))!;
	}

	public Task<PlayerTicket?> GetTicketByID(string cardID) => GetAsync(cardID);
	public async Task<PlayerTicket?> FindTicket(string playerTwitchID, string sessionId)
	{
		return await Collection.Find(ticket => ticket.TwitchUserID == playerTwitchID && ticket.SessionID == sessionId)
							   .FirstOrDefaultAsync();
	}

	public async Task<List<PlayerTicket>> GetAllTicketsForSession(string markedItemSessionID)
	{
		return await Collection.Find(ticket => ticket.SessionID == markedItemSessionID)
							   .ToListAsync();
	}

	public Task UpdateAsync(PlayerTicket ticket) => UpdateAsync(ticket.Id, ticket);

	Task IPlayerTicketRepo.UpdateListAsync(IEnumerable<PlayerTicket> tickets) => UpdateListAsync(tickets); 
}