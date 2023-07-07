using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class PlayerTicket : MongoDataEntry
{
	public string TwitchUserID { get; init; } = string.Empty;

	[BsonRepresentation(BsonType.ObjectId)]
	public string SessionID { get; init; } = string.Empty;
	public IEnumerable<PlayerTicketItem> Slots { get; init; } = new List<PlayerTicketItem>();
}