using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class BingoSessionItem
{
	public int ItemIndex { get; set; }

	[BsonRepresentation(BsonType.ObjectId)]
	public string SessionID { get; set; } = null!;

	public string DisplayText { get; set; } = null!;

	public bool Verified { get; set; }

}