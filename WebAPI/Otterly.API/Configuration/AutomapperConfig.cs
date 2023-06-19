using AutoMapper;
using Otterly.API.DataObjects.Bingo;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API.Configuration;

public class AutomapperConfig : Profile
{
	public AutomapperConfig()
	{
		CreateMap<OtterlyAppsUser, OtterlyAppsUserDTO>().ReverseMap();
		CreateMap<BingoSlot, BingoSlotDTO>().ReverseMap();
		CreateMap<BingoCard, BingoCardDTO>().ReverseMap();
		CreateMap<UserBingoOptions, UserBingoOptionsDTO>().ReverseMap();

		CreateMap<PlayerTicket, PlayerTicketDTO>().ReverseMap();
		CreateMap<BingoSession, BingoSessionDTO>().ReverseMap();
		CreateMap<BingoSessionItem, BingoSessionItemDTO>().ReverseMap();
		CreateMap<PlayerTicketItem, PlayerTicketItemDTO>().ReverseMap();


	}
}