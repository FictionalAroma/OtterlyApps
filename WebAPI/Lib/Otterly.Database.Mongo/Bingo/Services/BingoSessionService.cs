using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Otterly.API.ClientLib.Objects.Bingo;
using Otterly.API.ClientLib.Objects.User;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Configuration;
using Otterly.Database.ActivityData.Interfaces;

namespace Otterly.Database.ActivityData.Bingo.Services;

public class BingoSessionService : MongoServiceBase<BingoSession>, IBingoSessionRepo
{
	public BingoSessionService(MongoDBConfig config, MongoClient client) : 
		base(config, client, "bingo.streamerdata")
	{

	}

	public async Task<BingoSession?> CreateNewSession(BingoCardDTO card, OtterlyAppsUserDTO user)
	{
		try
		{
			var newSession = new BingoSession()
			{
				TwitchUserID = user.TwitchID,
				UserID = user.UserID,
				Active = true,
				FreeSpace = card.FreeSpace,
				Size = card.CardSize,
				CardTitle = card.TitleText,
			};

			await Collection.UpdateManyAsync(session => session.UserID == user.UserID &&
														 session.TwitchUserID == user.TwitchID &&
														 session.Active,
											 Builders<BingoSession>.Update.Set(session => session.Active, false));
			await CreateAsync(newSession);

			var createdSession = await FindActiveSessionForStreamer(user.TwitchID);
			if (createdSession == null || string.IsNullOrEmpty(createdSession.Id))
			{
				return null;
			}

			createdSession.SessionItems = card.Slots.Select(dto => new BingoSessionItem()
																   {
																	   ItemIndex = dto.SlotIndex,
																	   DisplayText = dto.DisplayText,
																	   SessionID = createdSession.Id,
																   }).ToList();
			await UpdateAsync(createdSession.Id, createdSession);

			return createdSession;

		}
		catch (Exception e)
		{
			return null;
		}

	}

	public async Task<BingoSession?> FindActiveSessionForStreamer(string streamerTwitchID)
	{
		return await Collection.Find(session => session.TwitchUserID == streamerTwitchID && session.Active).FirstOrDefaultAsync();
	}

	public async Task<BingoSession?> FindActiveSessionForUser(Guid userID)
	{
		return await Collection.Find(session => session.UserID == userID && session.Active).FirstOrDefaultAsync();

	}

	public Task UpdateAsync(BingoSession session) => base.UpdateAsync(session.Id, session);
	public Task<BingoSession?> GetByID(string requestSessionID) => GetAsync(requestSessionID);
}