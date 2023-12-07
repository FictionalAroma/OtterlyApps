using LDSoft.APIClient;
using Otterly.API.ClientLib.Objects.Bingo;

namespace Otterly.API.ClientLib.Messages.Bingo;

public class GetCardDetailsResponse : BaseResponse
{
    public BingoCardDTO Card { get; set; }
}