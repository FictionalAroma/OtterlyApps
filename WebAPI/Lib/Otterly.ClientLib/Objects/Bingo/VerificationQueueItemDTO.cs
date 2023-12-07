using System;
using System.Collections.Generic;

namespace Otterly.API.ClientLib.Objects.Bingo;

public class VerificationQueueItemDTO
{
	public int VerificationID { get; set; }
	public string SessionID { get; set; }
	public int ItemIndex { get; set; }
	public DateTime ActivatedDateTime { get; set; }
	public bool? Result { get; set; }
	public DateTime? VerifiedDateTime { get; set; }
	public DateTime ExpiryDateTime { get; set; }
	public List<VerificationPlayerLogDTO> PlayerLogs { get; set; }

}