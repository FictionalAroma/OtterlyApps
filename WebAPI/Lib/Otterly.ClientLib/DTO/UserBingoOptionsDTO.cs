namespace Otterly.API.ClientLib.DTO;

public class UserBingoOptionsDTO
{
    public Guid UserID { get; set; }

    public int ActiveBingoCard { get; set; }

    public int Cost { get; set; }

    public int Payout { get; set; }
}