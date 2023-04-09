using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Otterly.Database
{
    public class OtterlyAppsContext : IdentityDbContext
    {
        public OtterlyAppsContext(DbContextOptions<OtterlyAppsContext> options)
            : base(options)
        {
        }
    }
}