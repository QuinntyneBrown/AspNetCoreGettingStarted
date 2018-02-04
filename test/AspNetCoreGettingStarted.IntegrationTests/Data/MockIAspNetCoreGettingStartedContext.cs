using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Model;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreGettingStarted.IntegrationTests.Data
{
    public class MockAspNetCoreGettingStartedContext : DbContext, IAspNetCoreGettingStartedContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseInMemoryDatabase(databaseName: "AspNetCoreGettingStarted");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tenant>(b => {
                b.Property(t => t.TenantId)
                .HasDefaultValueSql("newsequentialid()");
            });
        }
    }
}
