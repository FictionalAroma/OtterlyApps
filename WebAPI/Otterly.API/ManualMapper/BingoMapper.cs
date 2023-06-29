using System.Collections.Generic;
using Otterly.API.DataObjects.Bingo;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.ManualMapper;

public static class BingoMapper
{
	public static BingoCard Map(BingoCardDTO dto)
    {
        return new BingoCard
        {
            CardID = dto.CardID.GetValueOrDefault(),
            CardName = dto.CardName,
            TitleText = dto.TitleText,
            CardSize = dto.CardSize,
            FreeSpace = dto.FreeSpace,
            Slots = dto.Slots.ConvertAll(Map)
        };
    }

	public static BingoCardDTO Map(BingoCard entity)
    {
        return new BingoCardDTO
        {
            CardID = entity.CardID,
            CardName = entity.CardName,
            TitleText = entity.TitleText,
            CardSize = entity.CardSize,
            FreeSpace = entity.FreeSpace,
            Slots = entity.Slots.ConvertAll(entitySlot => new BingoSlotDTO
            {
                SlotIndex = entitySlot.SlotIndex,
                CardID = entitySlot.CardID,
                DisplayText = entitySlot.DisplayText
            })
        };
    }

	public static List<BingoCardDTO> Map(List<BingoCard> entityList)
	{
		return entityList.ConvertAll(Map);
    }

	public static BingoSlot Map(BingoSlotDTO bingoSlotDTO)
    {
        return new BingoSlot
        {
            SlotIndex = bingoSlotDTO.SlotIndex,
            CardID = bingoSlotDTO.CardID,
            DisplayText = bingoSlotDTO.DisplayText
        };
    }
}