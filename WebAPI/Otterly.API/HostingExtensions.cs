﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth0.AspNetCore.Authentication;
using Auth0Net.DependencyInjection;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
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
using Otterly.API.DataObjects.User;
using Otterly.API.ExternalAPI;
using Otterly.API.ExternalAPI.Interfaces;

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
		services.AddAuth0AuthenticationClient(configuration =>
		{
			configuration.Domain = builder.Configuration["Auth0:Domain"];
			configuration.ClientId = builder.Configuration["Auth0:ClientId"];
			configuration.ClientSecret = builder.Configuration["Auth0:ClientSecret"];

		});
		services.AddAuth0ManagementClient().AddManagementAccessToken();
		services.AddScoped<ICardHandler, CardHandler>();
		services.AddScoped<IAccountHandler, AccountHandler>();
		services.AddScoped<IBingoGameHandler, BingoGameHandler>();
		services.AddScoped<IAuthManagementConnector, Auth0ManagementConnector>();



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
		builder.Services.AddAuthentication(Auth0Constants.AuthenticationScheme)
			   .AddJwtBearer(Auth0Constants.AuthenticationScheme, options =>
			   {
				   options.Authority = domain;
				   options.Audience = builder.Configuration["Auth0:Audience"];
				   options.TokenValidationParameters = new TokenValidationParameters
													   {
														   NameClaimType = ClaimTypes.NameIdentifier
													   };
			   })
			   .AddJwtBearer("twitch.ebs", options =>
			   {
				   options.TokenValidationParameters = new TokenValidationParameters()
													   {
														   ValidateLifetime = true,
														   ValidateAudience = false,
														   ValidateIssuer = false,
														   IssuerSigningKey =
															   new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Twitch.Bingo.Secret"])),
														   ValidateIssuerSigningKey = true,
														   
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