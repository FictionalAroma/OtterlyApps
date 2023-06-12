using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otterly.API.ClientLib.DTO;

public class BingoSlotDTO
{
    public int SlotIndex { get; set; }
    public int CardID { get; set; }
    public string DisplayText { get; set; }
}