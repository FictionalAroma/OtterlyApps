using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Configuration;
using Otterly.Database.ActivityData.Interfaces;

namespace Otterly.Database.ActivityData.Bingo.Services;

public class PlayerCardDataService : MongoServiceBase<PlayerTicket>, IPlayerCardDataService
{
	public PlayerCardDataService(MongoDBConfig config, MongoClient client, IMapper mapper) : base(config, client, "bingo.playercards", mapper)
	{


	}


	public async Task<PlayerTicket> CreatePlayerTicket(Guid playerTwitchID, string sessionId, IEnumerable<PlayerTicketItem> randomisedSlots)
	{
		var newTicket = new PlayerTicket()
						{
							SessionID = sessionId,
							TwitchUserID = playerTwitchID,
							Slots = randomisedSlots
						};
		await CreateAsync(newTicket);

		return (await FindTicket(playerTwitchID, sessionId))!;
	}

	public Task<PlayerTicket?> GetTicketByID(string cardID) => GetAsync(cardID);
	public async Task<PlayerTicket?> FindTicket(Guid playerTwitchID, string sessionId)
	{
		return await Collection.Find(ticket => ticket.TwitchUserID == playerTwitchID && ticket.SessionID == sessionId)
							   .FirstOrDefaultAsync();
	}

	public async Task<List<PlayerTicket>> GetAllTicketsForSession(string markedItemSessionID)
	{
		return await Collection.Find(ticket => ticket.SessionID == markedItemSessionID)
							   .ToListAsync();
	}
}