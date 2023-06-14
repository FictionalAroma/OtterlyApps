using Microsoft.EntityFrameworkCore;
using Otterly.Database.DataObjects;

namespace Otterly.Database
{
    public class OtterlyAppsContext : DbContext
    {

        public OtterlyAppsContext(DbContextOptions<OtterlyAppsContext> options)
            : base(options)
        {
			
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			

			builder.Entity<BingoCard>(typeBuilder =>
			{
				typeBuilder.HasMany<BingoSlot>(card => card.Slots)
						   .WithOne()
						   .HasPrincipalKey(card => card.CardID)
						   .HasForeignKey(slot => slot.CardID);
			});
		}

		public DbSet<OtterlyAppsUser> OtterlyAppsUsers { get; set; }
		public DbSet<BingoCard> BingoCards { get; set; }
        public DbSet<BingoSlot> BingoSlots { get; set; }
        public DbSet<UserBingoOptions> UserBingoOptions { get; set; }

    }
}