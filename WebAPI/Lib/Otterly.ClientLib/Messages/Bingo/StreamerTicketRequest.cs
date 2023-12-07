namespace Otterly.API.ClientLib.Messages.Bingo;

public class StreamerTicketRequest
{
	public string StreamerTwitchID { get; set; }  = string.Empty;
	public string PlayerTwitchID { get; set; }  = string.Empty;
}