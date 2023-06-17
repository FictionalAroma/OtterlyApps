using System;

namespace Otterly.API.ClientLib.Bingo;

public class MarkItemRequest
{
	public Guid PlayerTwitchID { get; set; }
	public string SessionID { get; set; }
	public int ItemIndex { get; set; }
}