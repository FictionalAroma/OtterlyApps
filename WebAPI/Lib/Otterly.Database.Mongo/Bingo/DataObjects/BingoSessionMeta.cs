﻿using System;

namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class BingoSessionMeta
{
	public int NumberTickets { get; set; }

	public int NumberWinners { get; set; }

	public DateTime StartDate { get; init; } = DateTime.Now;
}