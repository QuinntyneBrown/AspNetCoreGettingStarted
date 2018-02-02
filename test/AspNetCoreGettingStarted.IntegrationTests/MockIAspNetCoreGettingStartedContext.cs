using AspNetCoreGettingStarted.Data;
using AspNetCoreGettingStarted.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreGettingStarted.IntegrationTests
{
    public class MockAspNetCoreGettingStartedContext : DbContext, IAspNetCoreGettingStartedContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=AspNetCoreGettingStarted;Integrated Security=SSPI;");
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
