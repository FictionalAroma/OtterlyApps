﻿using System;

namespace Otterly.API.ClientLib.Bingo;

public class MarkItemRequest
{
	public string PlayerTwitchID { get; set; }
	public string SessionID { get; set; }
	public int ItemIndex { get; set; }
}