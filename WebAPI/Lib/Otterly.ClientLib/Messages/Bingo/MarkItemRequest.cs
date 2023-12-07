namespace Otterly.API.ClientLib.Messages.Bingo;

public class MarkItemRequest
{
	public string PlayerTwitchID { get; set; }
	public string SessionID { get; set; }
	public string ScreenName { get; set; }
	public int ItemIndex { get; set; }
}