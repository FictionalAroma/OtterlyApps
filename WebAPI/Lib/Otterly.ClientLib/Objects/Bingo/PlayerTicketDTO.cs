using System;
using System.Collections.Generic;

namespace Otterly.API.ClientLib.Objects.Bingo;

public class PlayerTicketDTO
{
    public string TwitchUserID { get; set; }  = string.Empty;

    public string SessionID { get; set; }  = string.Empty;
	public string TwitchDisplayName { get; set; } = string.Empty;
	public IEnumerable<PlayerTicketItemDTO> Slots { get; set; } = new List<PlayerTicketItemDTO>();
	public DateTime LastStampedDateTIme { get; set; }
}