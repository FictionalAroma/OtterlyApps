using Otterly.API.ClientLib.DTO;
using Otterly.ClientLib;

namespace Otterly.API.ClientLib.Bingo;

public class GetCardDetailsResponse : BaseResponse
{
    public BingoCardDTO Card { get; set; }
    public List<BingoSlotDTO> CardFields { get; set; } = new List<BingoSlotDTO>();
}