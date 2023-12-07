using LDSoft.APIClient;

namespace Otterly.API.ClientLib.Messages.Bingo;

public class GetCardDeatilsRequest : BaseRequest
{
	public int CardID { get; set; }
}