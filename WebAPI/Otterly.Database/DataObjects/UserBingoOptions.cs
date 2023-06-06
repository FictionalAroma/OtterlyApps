using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.DataObjects;

public class UserBingoOptions
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int UserIndex { get; set; }

	public Guid UserID { get; set; }

	public int ActiveBingoCard { get; set; }

	public int Cost { get; set; }

	public int Payout { get; set; }
}