using AutoMapper;
using Microsoft.Extensions.Options;
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


}