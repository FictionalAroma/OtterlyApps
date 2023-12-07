using LDSoft.APIClient;
using Otterly.API.ClientLib.Objects.Bingo;

namespace Otterly.API.ClientLib.Messages.Bingo;

public class InitialSetupResponse : BaseResponse
{
	public BingoSessionDTO? Session { get; set; }
	public PlayerTicketDTO? PlayerTicket { get; set; }
}