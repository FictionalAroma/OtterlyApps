
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Otterly.Site.StartupExtensions;

namespace Otterly.Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews().AddNewtonsoftJson();
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS")))
			{
				builder.ConfigureAWS();
			}
			builder.AddOtterlyAPIClient();
			builder.Services.AddHttpClient();
			builder.AddAuthentication();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
			app.UseCors("CorsPolicy");
			app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "bff/{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}