using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class PlayerTicket : MongoDataEntry
{
	public string TwitchUserID { get; set; } = string.Empty;

	[BsonRepresentation(BsonType.ObjectId)]
	public string SessionID { get; set; } = string.Empty;
	public IEnumerable<PlayerTicketItem> Slots { get; set; } = new List<PlayerTicketItem>();
}