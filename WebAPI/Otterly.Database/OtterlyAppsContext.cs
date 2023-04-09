using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Otterly.Database.DataObjects;

namespace Otterly.Database
{
    public class OtterlyAppsContext : IdentityDbContext
    {
        public DbSet<OtterlyAppsUser> OtterlyAppsUsers { get; set; }

        public OtterlyAppsContext(DbContextOptions<OtterlyAppsContext> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}
	}
}