using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Otterly.Database.ActivityData;

public class MongoDataEntry
{
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string? Id { get; set; }
}