using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Context
{
    public class AddProductDbContext: DbContext
    {
        public AddProductDbContext() { }
        public AddProductDbContext(DbContextOptions<AddProductDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }


        
    }
}
