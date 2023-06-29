using System;
using System.Collections.Generic;

namespace Otterly.API.DataObjects.Bingo;

public class PlayerTicketDTO
{
    public Guid TwitchUserID { get; set; }

    public string SessionID { get; set; }

	public IEnumerable<PlayerTicketItemDTO> Slots { get; set; } = new List<PlayerTicketItemDTO>();
}