using System.Collections.Generic;
using Otterly.API.DataObjects.Bingo;
using Otterly.ClientLib;

namespace Otterly.API.ClientLib.Bingo;

public class GetCardDetailsResponse : BaseResponse
{
    public BingoCardDTO Card { get; set; }
    public List<BingoSlotDTO> CardFields { get; set; } = new List<BingoSlotDTO>();
}