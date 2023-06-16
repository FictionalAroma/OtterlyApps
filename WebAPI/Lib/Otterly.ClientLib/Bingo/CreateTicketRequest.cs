namespace Otterly.API.ClientLib.Bingo;

public class CreateTicketRequest
{
	public Guid StreamerTwitchID { get; set; }
	public Guid PlayerTwitchID { get; set; }
}