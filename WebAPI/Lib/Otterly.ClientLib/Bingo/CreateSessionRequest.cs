namespace Otterly.API.ClientLib.Bingo;

public class CreateSessionRequest
{
	public Guid UserID { get; set; }
	public int CardID { get; set; }
}