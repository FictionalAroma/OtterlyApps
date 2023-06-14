using Otterly.API.ClientLib.DTO;

namespace Otterly.API.ClientLib.Bingo;

public class UpdateCardDetailsRequest : BaseRequest
{
	public BingoCardDTO CardDetails { get; set; }
}