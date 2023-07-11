using System.Collections.Generic;
using System.Linq;
using Otterly.API.DataObjects.Bingo;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.API.ManualMapper;

public class GameMapper
{
	public static PlayerTicketDTO Map(PlayerTicket ticket)
    {
        return new PlayerTicketDTO
        {
            TwitchUserID = ticket.TwitchUserID,
            SessionID = ticket.SessionID,
            Slots = ticket.Slots.Select(ticketSlot => new PlayerTicketItemDTO
            {
                Selected = ticketSlot.Selected,
                ItemIndex = ticketSlot.ItemIndex,
                SessionID = ticketSlot.SessionID,
                DisplayText = ticketSlot.DisplayText,
                Verified = ticketSlot.Verified
            })
        };
    }

    public static BingoSessionDTO Map(BingoSession session)
    {
        return new BingoSessionDTO
        {
            Size = session.Size,
            FreeSpace = session.FreeSpace,
            CardTitle = session.CardTitle,
            SessionItems = session.SessionItems.ConvertAll(sessionSessionItem => new BingoSessionItemDTO
            {
                ItemIndex = sessionSessionItem.ItemIndex,
                SessionID = sessionSessionItem.SessionID,
                DisplayText = sessionSessionItem.DisplayText,
                Verified = sessionSessionItem.Verified
            }),
			Active = session.Active,
            SessionID = session.Id ?? string.Empty
		};
    }

	public static BingoSessionMetaDTO Map(BingoSessionMetaDTO meta)
    {
        return new BingoSessionMetaDTO
        {
            NumberTickets = meta.NumberTickets,
            NumberWinners = meta.NumberWinners
        };
    }

    public static IEnumerable<PlayerTicketItem> Map(IEnumerable<BingoSessionItem> randomisedSlots)
    {
        return randomisedSlots.Select(randomisedSlot => new PlayerTicketItem
        {
            ItemIndex = randomisedSlot.ItemIndex,
            SessionID = randomisedSlot.SessionID,
            DisplayText = randomisedSlot.DisplayText,
            Verified = randomisedSlot.Verified
        }).ToList();
    }
}