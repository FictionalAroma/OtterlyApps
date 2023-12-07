namespace Otterly.API.ClientLib.Messages.Bingo;

public class VerifyItemRequest
{
	public string SessionID { get; set; }
	public int SessionItemIndex { get; set; }
	public int VerificationID { get; set; }
	public bool State { get; set; }
}