using Otterly.API.DataObjects.Bingo;
using Otterly.ClientLib;

namespace Otterly.API.ClientLib.Bingo;

public class CreateTicketResponse : BaseResponse
{
    public PlayerTicketDTO CreatedTicket { get; set; }
}