using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;

namespace Duc.Splitt.Identity
{
    public class SplittIdentityUser : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiry { get; set; }
    }
    public class SplittIdentityRole : IdentityRole<Guid>
    {

        //public const string Admin = "Admin";

        //public const string User = "User";
    }
    public class SplittIdentityDbContext : IdentityDbContext<SplittIdentityUser, SplittIdentityRole, Guid>, IDataProtectionKeyContext
    {
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        public SplittIdentityDbContext(DbContextOptions<SplittIdentityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional configuration if needed
            builder.Entity<DataProtectionKey>().ToTable("DataProtectionKeys");
            // Configure Id as GUID
            builder.Entity<SplittIdentityUser>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd(); // Generates a new GUID for each new use

            builder.Entity<SplittIdentityRole>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

        }
    }
}
