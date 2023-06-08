using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.ClientLib.Bingo.DTO;

public class BingoCardDTO
{
	public int CardID { get; set; }

	public Guid UserID { get; set; }
	public string CardName { get; set; }
	public string TitleText { get; set; }
	public int CardSize { get; set; }
	public bool FreeSpace { get; set; }
}