using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.UserData.DataObjects;

public class BingoSlot
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int SlotIndex { get; set; }
	public int CardID { get; set; }
	public string DisplayText { get; set; }
}