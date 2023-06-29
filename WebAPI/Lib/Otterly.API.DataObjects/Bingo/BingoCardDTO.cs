using System;
using System.Collections.Generic;

namespace Otterly.API.DataObjects.Bingo;

public class BingoCardDTO
{
    public int? CardID { get; init; }

    //public Guid UserID { get; set; }

    public string CardName { get; init; }
    public string TitleText { get; init; }
    public int CardSize { get; init; }
    public bool FreeSpace { get; init; }

    public List<BingoSlotDTO> Slots { get; init; }
}