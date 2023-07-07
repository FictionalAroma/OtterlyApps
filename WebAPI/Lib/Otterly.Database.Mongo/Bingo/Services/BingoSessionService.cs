﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Otterly.API.ClientLib;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.DataObjects.User;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Configuration;
using Otterly.Database.ActivityData.Interfaces;

namespace Otterly.Database.ActivityData.Bingo.Services;

public class BingoSessionService : MongoServiceBase<BingoSession>, IBingoSessionService
{
	public BingoSessionService(MongoDBConfig config, MongoClient client) : 
		base(config, client, "bingo.streamerdata")
	{

	}

	public async Task<BaseResponse> CreateNewSession(BingoCardDTO card, OtterlyAppsUserDTO user)
	{
		var response = new BaseResponse();
		try
		{
			var newSession = new BingoSession()
			{
				TwitchUserID = user.TwitchID,
				UserID = user.UserID,
				Active = true,
				FreeSpace = card.FreeSpace,
				Size = card.CardSize,
			};

			await Collection.UpdateManyAsync(session => session.UserID == user.UserID &&
														 session.TwitchUserID == user.TwitchID &&
														 session.Active,
											 Builders<BingoSession>.Update.Set(session => session.Active, false));
			await CreateAsync(newSession);

			var createdSession = await FindActiveSessionForStreamer(user.TwitchID);
			if (createdSession == null || string.IsNullOrEmpty(createdSession.Id))
			{
				response.SetError("Somehow could not find new created Bingo Session");
				return response;
			}

			createdSession.SessionItems = card.Slots.Select(dto => new BingoSessionItem()
																   {
																	   ItemIndex = dto.SlotIndex,
																	   DisplayText = dto.DisplayText,
																	   SessionID = createdSession.Id,
																	   Verified = false,
																   }).ToList();
			await UpdateAsync(createdSession.Id, createdSession);
		}
		catch (Exception e)
		{
			response.SetError(e.Message);
		}


		return new BaseResponse();
	}


	public async Task<BingoSession?> FindActiveSessionForStreamer(string streamerTwitchID)
	{
		return await Collection.Find(session => session.TwitchUserID == streamerTwitchID && session.Active).FirstOrDefaultAsync();
	}
}