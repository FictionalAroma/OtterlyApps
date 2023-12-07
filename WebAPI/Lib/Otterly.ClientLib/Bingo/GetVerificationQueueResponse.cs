using System.Collections.Generic;
using LDSoft.APIClient;
using Otterly.API.DataObjects.Bingo;

namespace Otterly.API.ClientLib.Bingo;

public class GetVerificationQueueResponse : BaseResponse
{
	public List<VerificationQueueItemDTO> Verifications { get; set; }
}