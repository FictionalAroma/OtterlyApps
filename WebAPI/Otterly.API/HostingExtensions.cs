using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Otterly.Database;
using Otterly.API.Configuration;
using Otterly.API.Handlers;
using Otterly.API.Handlers.Interfaces;

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


		return builder;
	}

	public static void ConfigureDatabase(this WebApplicationBuilder builder)
	{
		var connectionString = builder.Configuration.GetConnectionString("LocalTest") ??
							   throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		builder.Services.AddDbContext<OtterlyAppsContext>(options =>
		{
			options.UseMySQL(connectionString,
							 optionsBuilder => optionsBuilder.MigrationsAssembly("Otterly.API"));
		});

		//builder.Services.AddIdentityCore<OtterlyAppsUser>()
		//	   .AddEntityFrameworkStores<OtterlyAppsContext>();

	}

	public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
	{

		return builder;
	}

	public static WebApplicationBuilder ConfigureAutomapper(this WebApplicationBuilder builder)
	{
		builder.Services.AddAutoMapper(typeof(AutomapperConfig));
		return builder;
	}
}