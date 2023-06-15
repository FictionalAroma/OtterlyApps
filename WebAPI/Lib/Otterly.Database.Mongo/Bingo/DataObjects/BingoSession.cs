using System;
using Otterly.API.ClientLib.DTO;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class BingoSession : MongoDataEntry
{
	public Guid UserID { get; set; }
	public Guid TwitchUserID { get; set; }

	public BingoCardDTO GameCard { get; set; }

	public bool Active { get; set; } = true;
}