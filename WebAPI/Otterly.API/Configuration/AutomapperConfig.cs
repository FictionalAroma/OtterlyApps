using AutoMapper;
using Otterly.API.DataObjects.Bingo;
using Otterly.Database.DataObjects;

namespace Otterly.API.Configuration;

public class AutomapperConfig : Profile
{
	public AutomapperConfig()
	{
		CreateMap<OtterlyAppsUser, OtterlyAppsUserDTO>().ReverseMap();

		CreateMap<BingoSlot, BingoSlotDTO>().ReverseMap();
		
		
		CreateMap<BingoCard, BingoCardDTO>().ReverseMap();




		CreateMap<UserBingoOptions, UserBingoOptionsDTO>().ReverseMap();

	}
}