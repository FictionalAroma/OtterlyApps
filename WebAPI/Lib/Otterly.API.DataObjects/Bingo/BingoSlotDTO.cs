namespace Otterly.API.DataObjects.Bingo;

public class BingoSlotDTO
{
    public int SlotIndex { get; set; }
    public int CardID { get; set; }
    public string DisplayText { get; set; }
	public bool Deleted { get; set; }
}