using System;
using System.Collections.Generic;

namespace Otterly.API.DataObjects.Bingo;

public class BingoCardDTO
{
    public int CardID { get; set; }

    //public Guid UserID { get; set; }
    public string CardName { get; set; }
    public string TitleText { get; set; }
    public int CardSize { get; set; }
    public bool FreeSpace { get; set; }

    public List<BingoSlotDTO> Slots { get; set; }
}