using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Context
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext() { }
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Inventory> Inventory { get; set; }

    }
}
