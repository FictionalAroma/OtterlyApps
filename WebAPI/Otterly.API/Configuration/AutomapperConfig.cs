﻿using AutoMapper;
using Otterly.ClientLib.Bingo.DTO;
using Otterly.Database.DataObjects;

namespace Otterly.API.Configuration;

public class AutomapperConfig : Profile
{
	public AutomapperConfig()
	{
		CreateMap<OtterlyAppsUser, OtterlyAppsUserDTO>().ReverseMap();


		CreateMap<BingoCard, BingoCardDTO>().ReverseMap();
		CreateMap<BingoSlot, BingoSlotDTO>().ReverseMap();
		CreateMap<UserBingoOptions, UserBingoOptionsDTO>().ReverseMap();

	}
}