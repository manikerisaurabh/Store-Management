using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Context

{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<RegisterSuppliercs> Suppliers { get; set; }
        public DbSet<Categorycs> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<InventoryReceipts> InventoryReceipts { get; set;}

        public DbSet<Sales> Sales { get; set; }
    }
}
