﻿using System;
using System.Collections.Generic;

namespace Otterly.API.DataObjects.Bingo;

public class BingoSessionDTO
{
    public int Size { get; set; }
    public bool FreeSpace { get; set; }
    public List<BingoSessionItemDTO> SessionItems { get; set; } = new List<BingoSessionItemDTO>();

    public bool Active { get; set; } = true;
}