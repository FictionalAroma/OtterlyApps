using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Otterly.Database;
using Otterly.Database.DataObjects;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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

		var connectionString = builder.Configuration.GetConnectionString("LocalTest") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
		services.AddDbContext<OtterlyAppsContext>(options =>
		{
			options.UseMySQL(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly("Otterly.API"));
		});
		
		

		return builder;
	}

	public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
	{
		builder.Services.AddIdentityCore<OtterlyAppsUser>()
			   .AddEntityFrameworkStores<OtterlyAppsContext>();
		return builder;
	}
}