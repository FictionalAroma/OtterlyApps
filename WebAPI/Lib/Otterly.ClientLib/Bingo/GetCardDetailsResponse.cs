using Otterly.API.DataObjects.Bingo;

namespace Otterly.API.ClientLib.Bingo;

public class GetCardDetailsResponse : BaseResponse
{
    public BingoCardDTO Card { get; set; }
}