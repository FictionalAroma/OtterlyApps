using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Otterly.Database.DataObjects;

public class BingoSlot
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int SlotIndex { get; set; }
	public int CardID { get; set; }
	public string DisplayText { get; set; }
}