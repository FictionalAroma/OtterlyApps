using Otterly.ClientLib;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.API.ClientLib.Bingo;

public class CreateTicketResponse : BaseResponse
{
    public PlayerTicket CreatedTicket { get; set; }
}