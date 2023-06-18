using Otterly.API.DataObjects.Bingo;

namespace Otterly.API.ClientLib.Bingo;

public class UpdateCardDetailsRequest : BaseRequest
{
	public BingoCardDTO CardDetails { get; set; }
}