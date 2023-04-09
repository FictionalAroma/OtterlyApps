namespace Otterly.Database.DataObjects;

public class UserBingoOptions
{
	public Guid UserID { get; set; }

	public Guid ActiveBingoCard { get; set; }

	public int Cost { get; set; }

	public int Payout { get; set; }
}