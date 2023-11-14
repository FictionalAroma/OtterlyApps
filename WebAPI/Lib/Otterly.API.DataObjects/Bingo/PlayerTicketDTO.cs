using System;
using System.Collections.Generic;

namespace Otterly.API.DataObjects.Bingo;

public class PlayerTicketDTO
{
    public string TwitchUserID { get; set; }  = string.Empty;

    public string SessionID { get; set; }  = string.Empty;

	public IEnumerable<PlayerTicketItemDTO> Slots { get; set; } = new List<PlayerTicketItemDTO>();
	public DateTime LastStampedDateTIme { get; set; }
}