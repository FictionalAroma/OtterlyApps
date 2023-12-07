using LDSoft.APIClient;
using Otterly.API.ClientLib.Objects.Bingo;

namespace Otterly.API.ClientLib.Messages.Bingo;

public class UpdateCardDetailsRequest : BaseRequest
{
	public BingoCardDTO CardDetails { get; set; }
}