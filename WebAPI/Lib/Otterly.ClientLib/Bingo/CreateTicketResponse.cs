using LDSoft.APIClient;
using Otterly.API.DataObjects.Bingo;

namespace Otterly.API.ClientLib.Bingo;

public class CreateTicketResponse : BaseResponse
{
    public PlayerTicketDTO CreatedTicket { get; set; }
}