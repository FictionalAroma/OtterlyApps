using System;
using System.Security.Claims;
using Auth0Net.DependencyInjection;
using LDSoft.AWS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MySql.Data.MySqlClient;
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
	public static WebApplicationBuilder ConfigureAWS(this WebApplicationBuilder builder)
	{
		var options = builder.Configuration.GenerateAWSOptionsWithCreds();
		builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
		builder.Services.AddScoped(_ => options);

		builder.Host.ConfigureAppConfiguration(((_, configurationBuilder) =>
												   {
													   var secrets = builder.Configuration.GetSection("AWSConfig:SecretNames")
																			.Get<string[]>();
													   foreach (var s in secrets)
													   {
														   configurationBuilder.AddAmazonSecretsManager(options, options.Region.SystemName, s);
													   }
													   var connectionStrings = builder.Configuration.GetSection("AWSConfig:ConnectStringSecret")
																			.Get<string[]>();
													   foreach (var s in connectionStrings)
													   {
														   configurationBuilder.AddAmazonSecretsManagerConnectString(options, options.Region.SystemName, s);
													   }


												   }));

		return builder;
	}


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

		services.AddHttpClient<TwitchExtensionAPIConnector>();

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
		var dbString = builder.Configuration["DatabaseToUse"];
		var connectionString = builder.Configuration.GetConnectionString(dbString) ??
							   throw new InvalidOperationException($"Connection string {dbString} not found.");

		var connectionStringBuilder = new MySqlConnectionStringBuilder(connectionString);
		builder.Services.AddDbContext<OtterlyAppsContext>(options =>
		{
			options.UseMySQL(connectionString, optionsBuilder =>
			{
				optionsBuilder.MigrationsAssembly("Otterly.API");
				
			});

		});

		return builder;
		//builder.Services.AddIdentityCore<OtterlyAppsUser>()
		//	   .AddEntityFrameworkStores<OtterlyAppsContext>();
	}

	public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
	{
		builder.Services.AddAuthentication(Constants.Auth0PolicyName)
			   .AddJwtBearer(Constants.Auth0PolicyName, options =>
			   {
				   var domain = $"https://{builder.Configuration["Auth0:Domain"]}/";

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

		
		var settings = MongoClientSettings.FromConnectionString(mongoConfig.ConnectionString);
		// Set the ServerApi field of the settings object to Stable API version 1
		settings.ServerApi = new ServerApi(ServerApiVersion.V1);
		settings.RetryWrites = true;

		settings.Credential = MongoCredential.CreateCredential("admin", mongoConfig.Username, mongoConfig.Password);
		settings.UseTls = true;

		builder.Services.AddSingleton(_ => mongoConfig);
		builder.Services.AddSingleton(_ => new MongoClient(settings));

		builder.Services.AddSingleton<IBingoSessionRepo, BingoSessionService>();
		builder.Services.AddSingleton<IPlayerTicketRepo, PlayerTicketService>();

		return builder;
	}
}