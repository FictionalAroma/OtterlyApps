using System;
using System.Collections.Generic;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class BingoSession : MongoDataEntry
{
	public Guid UserID { get; init; }
	public string TwitchUserID { get; init; }  = string.Empty;

	public string CardTitle { get; set; } = string.Empty;

	public int Size { get; init; }
	public bool FreeSpace { get; init; }
	public List<BingoSessionItem> SessionItems { get; set; } = new List<BingoSessionItem>();

	public bool Active { get; set; } = true;

	public TimeSpan StampTimeLock { get; set; } = TimeSpan.FromMinutes(1.0);

	public TimeSpan VerificationTimeout { get; set; } = TimeSpan.FromMinutes(15.0);

	public TimeSpan VerificationGutterTime { get; set; } = TimeSpan.FromMinutes(15.0);

	public 


	public BingoSessionMeta Meta { get; set; } = new BingoSessionMeta();
}