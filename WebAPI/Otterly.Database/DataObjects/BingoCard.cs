using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.Database.DataObjects;

public class BingoCard
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid CardID { get; set; }
	public string CardName { get; set; }
	public string TitleText { get; set; }
	public int CardSize { get; set; }
	public bool FreeSpace { get; set; }

	public bool Deleted { get; set; }
	
}