using System.Collections.Generic;
using Otterly.API.ClientLib.Objects.Bingo;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.ManualMapper;

public static class BingoMapper
{
	public static BingoCard MapFromDTO(BingoCardDTO dto)
	{
        return new BingoCard
        {
            CardID = dto.CardID.GetValueOrDefault(),
            CardName = dto.CardName,
            TitleText = dto.TitleText,
            CardSize = dto.CardSize,
            FreeSpace = dto.FreeSpace,
            Slots = dto.Slots.ConvertAll(MapFromDTO)
        };
	}

	public static BingoCardDTO MapToDTO(BingoCard entity)
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
                DisplayText = entitySlot.DisplayText,
                Deleted = entitySlot.Deleted,
			})
        };
    }

	public static List<BingoCardDTO> MapToDTO(List<BingoCard> entityList)
	{
		return entityList.ConvertAll(MapToDTO);
    }

	public static BingoSlot MapFromDTO(BingoSlotDTO bingoSlotDTO)
	{
		return new BingoSlot
			   {
				   SlotIndex = bingoSlotDTO.SlotIndex,
				   CardID = bingoSlotDTO.CardID,
				   DisplayText = bingoSlotDTO.DisplayText
			   };
    }

	public static BingoSlot Map(this BingoSlot slotToUpdate, BingoSlotDTO bingoSlotDTO)
	{
		slotToUpdate.SlotIndex = bingoSlotDTO.SlotIndex;
		slotToUpdate.CardID = bingoSlotDTO.CardID;
		slotToUpdate.DisplayText = bingoSlotDTO.DisplayText;
		slotToUpdate.Deleted = bingoSlotDTO.Deleted;
		return slotToUpdate;
	}

}