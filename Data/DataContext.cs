using DotNetCoreGettingStarted.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetCoreGettingStarted.Data
{
    public interface IDataContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
    }

    
    public class DataContext: DbContext, IDataContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) {
            dbContextOptionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=DotNetCoreGettingStarted;Integrated Security=SSPI;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

        }
    }
}
