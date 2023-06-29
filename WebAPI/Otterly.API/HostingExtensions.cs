using System;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AutoMapper.EntityFrameworkCore;
using AutoMapper.Collection;
using AutoMapper.Collection.Configuration;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Otterly.API.DataObjects.Bingo;
using Otterly.API.Handlers;
using Otterly.API.Handlers.Interfaces;
using Otterly.Database.ActivityData.Bingo.Services;
using Otterly.API.Handlers.Bingo;
using Otterly.Database.ActivityData.Bingo.DataObjects;
using Otterly.Database.ActivityData.Configuration;
using Otterly.Database.ActivityData.Interfaces;
using Otterly.Database.UserData;
using Otterly.Database.UserData.DataObjects;

namespace Otterly.API;

public static class HostingExtensions
{
	public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
	{
		var config = builder.Configuration;
		var services = builder.Services;

		services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		services.AddScoped<ICardHandler, CardHandler>();
		services.AddScoped<IAccountHandler, AccountHandler>();
		services.AddScoped<IBingoGameHandler, BingoGameHandler>();


		return builder;
	}

	public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
	{
		var connectionString = builder.Configuration.GetConnectionString("LocalTest") ??
							   throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContext<OtterlyAppsContext>(options =>
		{
			options.UseMySQL(connectionString,
							 optionsBuilder => optionsBuilder.MigrationsAssembly("Otterly.API"));
		});

		return builder;
		//builder.Services.AddIdentityCore<OtterlyAppsUser>()
		//	   .AddEntityFrameworkStores<OtterlyAppsContext>();
	}

	public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
	{
		var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			   .AddJwtBearer(options =>
			   {
				   options.Authority = domain;
				   options.Audience = builder.Configuration["Auth0:Audience"];
				   options.TokenValidationParameters = new TokenValidationParameters
													   {
														   NameClaimType = ClaimTypes.NameIdentifier
													   };
			   });
		return builder;
	}

	public static WebApplicationBuilder ConfigureAutomapper(this WebApplicationBuilder builder)
	{
		builder.Services.AddAutoMapper((provider, mapper) =>
									   {
										   
										   mapper.AddCollectionMappers();
										   mapper.UseEntityFrameworkCoreModel<OtterlyAppsContext>(provider);

										   mapper.CreateMap<OtterlyAppsUser, OtterlyAppsUserDTO>().ReverseMap();
										   mapper.CreateMap<BingoSlot, BingoSlotDTO>();

										   mapper.CreateMap<BingoSlotDTO,BingoSlot>()
												 .EqualityComparison((dto, slot) => dto.CardID == slot.CardID && dto.SlotIndex == slot.SlotIndex);

										   mapper.CreateMap<BingoCard, BingoCardDTO>();
										   mapper.CreateMap<BingoCardDTO, BingoCard>();

										   mapper.CreateMap<UserBingoOptions, UserBingoOptionsDTO>().ReverseMap();

										   mapper.CreateMap<PlayerTicket, PlayerTicketDTO>().ReverseMap();
										   mapper.CreateMap<BingoSession, BingoSessionDTO>().ReverseMap();
										   mapper.CreateMap<BingoSessionItem, BingoSessionItemDTO>().ReverseMap();
										   mapper.CreateMap<PlayerTicketItem, PlayerTicketItemDTO>().ReverseMap();

									   },
									   typeof(AutomapperConfig).Assembly, 
									   typeof(Otterly.API.Configuration.AutomapperConfig).Assembly, typeof(OtterlyAppsContext).Assembly);
		return builder;
	}
	public static WebApplicationBuilder ConfigureMongoAccessServices(this WebApplicationBuilder builder)
	{

		var mongoConfig = builder.Configuration.GetSection("MongoDBConfig").Get<MongoDBConfig>();
		builder.Services.AddSingleton(_ => mongoConfig);
		builder.Services.AddSingleton(_ => new MongoClient(mongoConfig.ConnectionString));

		builder.Services.AddSingleton<IBingoSessionService, BingoSessionService>();
		builder.Services.AddSingleton<IPlayerCardDataService, PlayerCardDataService>();

		return builder;
	}
}