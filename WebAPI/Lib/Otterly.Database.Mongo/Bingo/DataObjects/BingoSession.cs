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

	public int StampTimeLock { get; set; } = 1;

	public int VerificationTimeoutMinutes { get; set; } = 15;

	public int VerificationGutterTime { get; set; } = 15;



	public BingoSessionMeta Meta { get; set; } = new BingoSessionMeta();
}