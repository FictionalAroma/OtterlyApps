using System.ComponentModel.DataAnnotations;

namespace Otterly.Database.DataObjects;

public class UserBingoOptions
{

	[Key]
	public Guid UserID { get; set; }

	public int ActiveBingoCard { get; set; }

	public int Cost { get; set; }

	public int Payout { get; set; }
}