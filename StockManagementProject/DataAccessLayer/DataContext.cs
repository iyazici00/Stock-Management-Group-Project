using StockManagementProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockManagementProject.DataAccessLayer
{
    internal class DataContext:DbContext
    {
        public DataContext() : base("dbConnection") { }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<WarehouseProductStock> WarehouseProductStock { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>()
                        .HasRequired(m => m.ShipperWarehouse)
                        .WithMany(t => t.ShipperShipment)
                        .HasForeignKey(m => m.ShipperWarehouseId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Shipment>()
                        .HasRequired(m => m.ReceiverWarehouse)
                        .WithMany(t => t.ReceiverShipment)
                        .HasForeignKey(m => m.ReceiverWarehouseId)
                        .WillCascadeOnDelete(false);
        }

    }
}
