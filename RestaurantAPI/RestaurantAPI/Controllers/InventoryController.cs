using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Context;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public InventoryController(InventoryDbContext context)
        {
            _context = context;
        }
        [HttpGet("getInventory")]
        public async Task<ActionResult<IEnumerable<Inventory>>> getInventoryData()
        {
            return await _context.Inventory.ToListAsync();
        }

        [HttpPost("addInventory")]
        public async Task<ActionResult> AddInventory(Inventory inventory)
        {
            var total = inventory.Quantity * inventory.ProductCost;
            inventory.Total = total;
            _context.Inventory.Add(inventory);
            _context.SaveChanges();
            return Ok(inventory);
        }
    }
}
