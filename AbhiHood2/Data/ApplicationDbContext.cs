using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbhiHood2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Models.PostedUserData> PostedUserData { get; set; }
        public DbSet<Models.UserZipCodeSubscription> UserZipCodeSubscription { get; set; }
    }
}