using Microsoft.EntityFrameworkCore;
using OTPGenerator.Infrastructure.Entities;
using OTPGenerator.Infrastructure.EntityConfigurations;

namespace OTPGenerator.Infrastructure
{
    public class OTPGeneratorDbContext : DbContext
    {
        public OTPGeneratorDbContext(DbContextOptions<OTPGeneratorDbContext> options): base(options)
        {
        }

        public DbSet<OTPCode> OTPCode { get; set; }
        public DbSet<Tenant> Tenant { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OTPCodeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TenantEntityConfiguration());   
        }
    }
}
