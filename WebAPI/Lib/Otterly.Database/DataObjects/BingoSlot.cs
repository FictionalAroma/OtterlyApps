using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Otterly.Database.UserData.DataObjects;

[Table("bingoslots")]

[PrimaryKey("SlotIndex","CardID" )]
public class BingoSlot
{
	
	public int SlotIndex { get; set; }
	public int CardID { get; set; }
	public string DisplayText { get; set; }

	public bool Deleted { get; set; }
}