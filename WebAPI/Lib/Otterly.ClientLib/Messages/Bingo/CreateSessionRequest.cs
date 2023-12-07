using LDSoft.APIClient;

namespace Otterly.API.ClientLib.Messages.Bingo;

public class CreateSessionRequest : BaseRequest
{
	public int CardID { get; set; }
}