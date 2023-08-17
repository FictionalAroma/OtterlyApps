using LDSoft.APIClient;
using Otterly.API.DataObjects.Bingo;

namespace Otterly.API.ClientLib.Bingo;

public class CreateSessionResponse : BaseResponse
{
	public BingoSessionDTO CreatedSession { get; set; }
}