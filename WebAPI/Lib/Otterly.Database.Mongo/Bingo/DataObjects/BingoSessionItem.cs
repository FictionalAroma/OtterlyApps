using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class BingoSessionItem
{
	public int ItemIndex { get; init; }

	[BsonRepresentation(BsonType.ObjectId)]
	public string SessionID { get; init; } = null!;

	public string DisplayText { get; init; } = null!;

	public bool Verified { get; set; }

}