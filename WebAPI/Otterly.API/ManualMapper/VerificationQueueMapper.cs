using Otterly.API.ClientLib.Objects.Bingo;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.ManualMapper;

public static class VerificationQueueMapper
{
	public static VerificationQueueItemDTO MapToDTO(this VerificationQueueItem item)
	{
		return new VerificationQueueItemDTO()
			   {
				   ItemIndex = item.ItemIndex,
				   SessionID = item.SessionID,
				   ActivatedDateTime = item.ActivatedDateTime,
				   ExpiryDateTime = item.ExpiryDateTime,
				   VerifiedDateTime = item.VerifiedDateTime,
				   Result = item.Result,
				   VerificationID = item.VerificationID,
				   PlayerLogs = item.PlayerLogs.ConvertAll(MapToDTO)
			   };
	}

	private static VerificationPlayerLogDTO MapToDTO(VerificationQueuePlayerLog input)
	{
		return new VerificationPlayerLogDTO()
			   {
				   ItemIndex = input.ItemIndex,
				   VerificationID = input.VerificationID,
				   PlayerID = input.PlayerID,
				   TicketID = input.TicketID,
			   };
	}
}