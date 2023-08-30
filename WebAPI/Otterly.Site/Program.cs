
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
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
			builder.Services.AddCors(options =>
			{
                options.AddDefaultPolicy(policyBuilder =>
				{
					var hosts = builder.Configuration.GetSection("CORSHosts").Get<string[]>();
					policyBuilder.WithOrigins(hosts).SetIsOriginAllowedToAllowWildcardSubdomains();
					policyBuilder.AllowAnyMethod();
					policyBuilder.AllowAnyHeader();
				});
			});
			builder.ConfigureAWS();
			builder.AddClientAppConfig();
			builder.AddOtterlyAPIClient();
			builder.Services.AddHttpClient();
			builder.AddAuthentication();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsProduction())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseForwardedHeaders(new ForwardedHeadersOptions
										{
											ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
										});
				app.UseHsts();

			}
			else
			{
				app.UseHttpsRedirection();
			}

			app.UseCors();


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