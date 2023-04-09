using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Otterly.Database.DataObjects;

namespace Otterly.Database
{
    public class OtterlyAppsContext : IdentityDbContext
    {

        public OtterlyAppsContext(DbContextOptions<OtterlyAppsContext> options)
            : base(options)
        {
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}

		public DbSet<OtterlyAppsUser> OtterlyAppsUsers { get; set; }
		public DbSet<BingoCard> BingoCards { get; set; }
        public DbSet<BingoSlot> BingoSlots { get; set; }
        public DbSet<UserBingoOptions> UserBingoOptions { get; set; }

    }
}