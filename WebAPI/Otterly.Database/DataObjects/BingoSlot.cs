using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.DataObjects;

public class BingoSlot
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid SlotID { get; set; }
	public string DisplayText { get; set; }
}