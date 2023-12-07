namespace Otterly.API.ClientLib.Objects.Bingo;

public class BingoSlotDTO
{
    public int SlotIndex { get; set; }
    public int CardID { get; set; }
    public string DisplayText { get; set; }
	public bool Deleted { get; set; }
}