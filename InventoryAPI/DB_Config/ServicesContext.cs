using InventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InventoryAPI.DB_Config
{
    public class ServicesContext : DbContext
    {
        public ServicesContext()
            : base("Inventory-Sales")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ServicesContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("api");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public DbSet<DailyStats> dailyStats { get; set; }
        public DbSet<MonthlyStats> monthlyStats { get; set; }
    }
}