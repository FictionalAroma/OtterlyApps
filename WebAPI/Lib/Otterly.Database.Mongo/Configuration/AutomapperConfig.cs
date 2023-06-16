using AutoMapper;
using Otterly.Database.ActivityData.Bingo.DataObjects;

namespace Otterly.Database.ActivityData.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig() : base()
	{
		CreateMap<BingoSessionItem, PlayerTicketItem>().ReverseMap();
	}
}