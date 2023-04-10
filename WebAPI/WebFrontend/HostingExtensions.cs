using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Otterly.Database.DataObjects;
using Otterly.Database;
using WebFrontend.PageServices;
using WebFrontend.Areas.Identity;

namespace WebFrontend
{
    public static class HostingExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("LocalTest") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContextFactory<OtterlyAppsContext>(options =>
			{
				options.UseMySQL(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly("WebFrontend"));
			});
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddIdentity<OtterlyAppsUser, IdentityRole>()
				   .AddEntityFrameworkStores<OtterlyAppsContext>()
				   .AddDefaultTokenProviders()
				   .AddDefaultUI();
            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)

            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            var twtichConfig = builder.Configuration.GetSection("TwitchAuth") ?? throw new InvalidOperationException("Twitch Auth not found");

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "TwitchAuth";
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/signin";
                options.LogoutPath = "/signout";
            })
            .AddTwitch("TwitchAuth", options =>
            {
                options.ClientId = twtichConfig["ClientID"];
                options.ClientSecret = twtichConfig["ClientSecret"];
            });
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
			builder.Services.AddHttpContextAccessor();
			builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<OtterlyAppsUser>>();
            // and now our services
            builder.Services.AddScoped<BingoPageService>();

            return builder;

        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            return app;
        }
    }
}
