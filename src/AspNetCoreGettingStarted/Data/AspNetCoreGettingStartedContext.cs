using AspNetCoreGettingStarted.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreGettingStarted.Data
{
    public interface IAspNetCoreGettingStartedContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Tenant> Tenants { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<Dashboard> Dashboards { get; set; }
        DbSet<DashboardTile> DashboardTiles { get; set; }
        DbSet<Tile> Tiles { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

    
    public class AspNetCoreGettingStartedContext: DbContext, IAspNetCoreGettingStartedContext
    {
        public AspNetCoreGettingStartedContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardTile> DashboardTiles { get; set; }
        public DbSet<Tile> Tiles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Tenant>(b => {
                b.Property(t => t.TenantId)
                .HasDefaultValueSql("newsequentialid()");
            });

            modelBuilder.Entity<DigitalAsset>().HasQueryFilter(d => !d.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();
            foreach(var item in ChangeTracker.Entries().Where( e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
