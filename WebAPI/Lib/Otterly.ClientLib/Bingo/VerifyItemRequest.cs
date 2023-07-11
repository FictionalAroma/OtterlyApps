namespace Otterly.API.ClientLib.Bingo;

public class VerifyItemRequest
{
	public string SessionID { get; set; }
	public int ItemIndex { get; set; }
	public bool State { get; set; }
}