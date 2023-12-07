using System;

namespace Otterly.API.ClientLib.Objects.Bingo;

public class BingoSessionMetaDTO
{
	public int NumberTickets { get; init; }

	public int NumberWinners { get; init; }

	public DateTime StartDate { get; init; }

}