using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
            
        }
        override protected void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId= "1f8b0c3e-4d2a-4b5c-9f3e-7c8d9e0f1a2b";
            var writerRoleId = "2f8b0c3e-4d2a-4b5c-9f3e-7c8d9e0f1a2b";

            var roles = new List<IdentityRole>
            {
                new IdentityRole {Id=readerRoleId, ConcurrencyStamp=readerRoleId, Name = "Reader", NormalizedName = "Reader".ToUpper() },
                new IdentityRole {Id=writerRoleId, ConcurrencyStamp=readerRoleId, Name = "Writer", NormalizedName = "Writer".ToUpper() }

            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
