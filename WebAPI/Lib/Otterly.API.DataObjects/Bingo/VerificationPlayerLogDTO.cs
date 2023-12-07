using System;
using System.Collections.Generic;

namespace Otterly.API.DataObjects.Bingo;

public class VerificationPlayerLogDTO
{
	public string PlayerID { get; set; }
	public string TicketID { get; set; }
	public int ItemIndex { get; set; }
	public int VerificationID { get; set; }

}