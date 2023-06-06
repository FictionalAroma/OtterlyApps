using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.ClientLib.Bingo.DTO;

public class BingoSlotDTO
{
	public int SlotIndex { get; set; }
	public int CardID { get; set; }
	public string DisplayText { get; set; }
}