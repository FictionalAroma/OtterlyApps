using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Otterly.Database;
using Otterly.Database.DataObjects;
using WebFrontend.Areas.Identity;

namespace WebFrontend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.ConfigureServices();

            var app = builder.Build();
            app.ConfigurePipeline();


            app.Run();
        }

    }
}