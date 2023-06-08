using Otterly.ClientLib.Bingo.DTO;

namespace Otterly.ClientLib.Bingo.Messaging;

public class GetCardDetails : BaseResponse
{
	public BingoCardDTO Card { get; set; }
	public List<BingoSlotDTO> CardFields { get; set; } = new List<BingoSlotDTO>();
}