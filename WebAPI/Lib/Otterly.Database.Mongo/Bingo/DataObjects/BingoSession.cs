using System;
using System.Collections.Generic;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class BingoSession : MongoDataEntry
{
	public Guid UserID { get; set; }
	public string TwitchUserID { get; set; }  = string.Empty;

	public int Size { get; set; }
	public bool FreeSpace { get; set; }
	public List<BingoSessionItem> SessionItems { get; set; } = new List<BingoSessionItem>();

	public bool Active { get; set; } = true;
}