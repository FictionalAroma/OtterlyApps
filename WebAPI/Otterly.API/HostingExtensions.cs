﻿using System;
using System.Buffers.Text;
using System.Security.Claims;
using System.Text;
using Auth0.AspNetCore.Authentication;
using Auth0Net.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Otterly.API.Handlers;
using Otterly.API.Handlers.Interfaces;
using Otterly.Database.ActivityData.Bingo.Services;
using Otterly.API.Handlers.Bingo;
using Otterly.Database.ActivityData.Configuration;
using Otterly.Database.ActivityData.Interfaces;
using Otterly.Database.UserData;
using Otterly.API.ExternalAPI;
using Otterly.API.ExternalAPI.Interfaces;
using Otterly.API.Configuration;

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

		services.AddCors(options =>
		{
			options.AddDefaultPolicy(policyBuilder =>
			{
				policyBuilder.AllowAnyOrigin();
				policyBuilder.AllowAnyHeader();
				policyBuilder.AllowAnyMethod();

			});
		});

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
			   .AddJwtBearer(Constants.TwitchAuthPolicyName, options =>
			   {
				   options.TokenValidationParameters = new TokenValidationParameters()
													   {

														   ValidateLifetime = true,
														   ValidateAudience = false,
														   ValidateIssuer = false,
														   IssuerSigningKey =
															   new SymmetricSecurityKey(Convert.FromBase64String(builder.Configuration["Twitch:Bingo:Secret"])),
														   ValidateIssuerSigningKey = true
													   };
			   });
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