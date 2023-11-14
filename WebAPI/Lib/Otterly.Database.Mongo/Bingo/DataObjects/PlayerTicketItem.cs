using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class PlayerTicketItem : BingoSessionItemBase
{
	public bool Selected { get; set; }
	public bool Verified { get; set; }


	[BsonRepresentation(BsonType.DateTime)]
	public DateTime SelectedDateTime { get; set; }

	public static PlayerTicketItem CreateFreeSpace(string sessionID) => new PlayerTicketItem()
												{
													Verified = true,
													SessionID = sessionID,
													DisplayText = "Free Space",
													ItemIndex = -1,
													Selected = true,
												};

}

