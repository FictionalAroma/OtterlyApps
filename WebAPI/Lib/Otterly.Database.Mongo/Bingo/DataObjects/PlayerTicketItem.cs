namespace Otterly.Database.ActivityData.Bingo.DataObjects;

public class PlayerTicketItem : BingoSessionItem
{
	public bool Selected { get; set; }

	public static PlayerTicketItem CreateFreeSpace(string sessionID) => new PlayerTicketItem()
												{
													Verified = true,
													SessionID = sessionID,
													DisplayText = "Free Space",
													ItemIndex = -1,
													Selected = true,
												};

}

