using LDSoft.APIClient;
using Otterly.API.ClientLib.Objects.Bingo;

namespace Otterly.API.ClientLib.Messages.Bingo;

public class CreateSessionResponse : BaseResponse
{
	public BingoSessionDTO CreatedSession { get; set; }
}