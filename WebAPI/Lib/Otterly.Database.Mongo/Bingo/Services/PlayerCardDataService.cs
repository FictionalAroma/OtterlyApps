using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Configuration;

namespace Otterly.Database.ActivityData.Bingo.Services;

public class PlayerCardDataService : MongoService<PlayerCard>
{
	public PlayerCardDataService(IOptions<MongoDBConfig> config, MongoClient client) : base(config, client, "bingo.playercards")
	{


	}


}