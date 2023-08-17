using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.UserData.DataObjects;

[Table("bingocards")]
public class BingoCard
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int CardID { get; set; }


	[ForeignKey("UserID")]
	public Guid UserID { get; set; }
	public string CardName { get; set; }
	public string TitleText { get; set; }
	public int CardSize { get; set; }
	public bool FreeSpace { get; set; }

	public bool Deleted { get; set; }

	public List<BingoSlot> Slots { get; set; }
}