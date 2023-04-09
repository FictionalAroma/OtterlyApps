using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Otterly.Database.DataObjects;
using Otterly.Database;
using WebFrontend.Areas.Identity;

namespace WebFrontend
{
    public static class HostingExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("LocalTest") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<OtterlyAppsContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<OtterlyAppsUser, IdentityRole>()
                .AddEntityFrameworkStores<OtterlyAppsContext>()
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
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<OtterlyAppsUser>>();

            return builder;

        }
    }
}
