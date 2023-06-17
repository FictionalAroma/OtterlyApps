using Otterly.API.DataObjects.Bingo;
using Otterly.ClientLib;

namespace Otterly.API.ClientLib.Bingo;

public class InitialSetupResponse : BaseResponse
{
	public BingoSessionDTO? Session { get; set; }
	public PlayerTicketDTO? PlayerTicket { get; set; }
}