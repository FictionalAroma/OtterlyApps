using LDSoft.APIClient;

namespace Otterly.API.ClientLib.Bingo;

public class GetCardDeatilsRequest : BaseRequest
{
	public int CardID { get; set; }
}