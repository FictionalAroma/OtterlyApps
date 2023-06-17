using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class BingoSessionItem
{
	public int ItemIndex { get; set; }

	[BsonRepresentation(BsonType.ObjectId)]
	public string SessionID { get; set; }

	public string DisplayText { get; set; }

	public bool Verified { get; set; }

}