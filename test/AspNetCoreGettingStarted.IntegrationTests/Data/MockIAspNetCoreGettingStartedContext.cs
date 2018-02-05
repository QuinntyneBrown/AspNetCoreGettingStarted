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
        public DbSet<Dashboard> Dashboards { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public DbSet<DashboardTile> DashboardTiles { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public DbSet<Tile> Tiles { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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
