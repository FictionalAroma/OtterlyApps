using System.Collections.Generic;
using LDSoft.APIClient;
using Otterly.API.ClientLib.Objects.Bingo;

namespace Otterly.API.ClientLib.Messages.Bingo;

public class GetVerificationQueueResponse : BaseResponse
{
	public List<VerificationQueueItemDTO> Verifications { get; set; }
}