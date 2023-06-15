using System;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class PlayerTicket : MongoDataEntry
{
	public Guid TwitchUserID { get; set; }
}