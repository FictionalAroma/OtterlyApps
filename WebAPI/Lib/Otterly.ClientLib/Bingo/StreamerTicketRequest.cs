using System;

namespace Otterly.API.ClientLib.Bingo;

public class StreamerTicketRequest
{
	public Guid StreamerTwitchID { get; set; }
	public Guid PlayerTwitchID { get; set; }
}