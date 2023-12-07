using LDSoft.APIClient;
using Otterly.API.ClientLib.Objects.Bingo;

namespace Otterly.API.ClientLib.Messages.Bingo;

public class CreateTicketResponse : BaseResponse
{
    public PlayerTicketDTO CreatedTicket { get; set; }
}