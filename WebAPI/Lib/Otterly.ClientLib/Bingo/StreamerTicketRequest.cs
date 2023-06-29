using System;

namespace Otterly.API.ClientLib.Bingo;

public class StreamerTicketRequest
{
	public string StreamerTwitchID { get; set; }  = string.Empty;
	public string PlayerTwitchID { get; set; }  = string.Empty;
}