﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Otterly.API.ClientLib.DTO;
using Otterly.ClientLib;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Configuration;

namespace Otterly.Database.ActivityData.Bingo.Services;

public class BingoSessionService :MongoService<BingoSession>
{
	public BingoSessionService(IOptions<MongoDBConfig> config, MongoClient client, IMapper mapper) : 
		base(config, client, "bingo.streamerdata", mapper)
	{

	}

	public async Task<BaseResponse> CreateNewSession(BingoCardDTO card, OtterlyAppsUserDTO user)
	{
		var newSession = new BingoSession()
						 {
							 GameCard = card,
							 TwitchUserID = user.TwitchID,
							 UserID = user.UserID
						 };

		await CreateAsync(newSession);

		await Collection.UpdateManyAsync((session => session.UserID == user.UserID &&
													 session.TwitchUserID == user.TwitchID &&
													 session.Active),
										 Builders<BingoSession>.Update.Set(session => session.Active, false));

		return new BaseResponse();
	}
}