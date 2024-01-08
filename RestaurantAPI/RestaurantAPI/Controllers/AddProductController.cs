using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Context;
using RestaurantAPI.Migrations;
using RestaurantAPI.Models;
using System.Xml.Serialization;
namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AddProductController : ControllerBase
    {
        private readonly AddProductDbContext _context;

       // private readonly DataContext _dataContext;
        public AddProductController(AddProductDbContext context)
        {
            _context = context;

           // _dataContext = dataContext;
        }

       
        [HttpGet("getAllProducts")]

        public async Task<ActionResult<IEnumerable<Product>>> getAllProduct(int  cataegoryId)
        {
            var weapons = await _context.Products
                                      .Where(w => w.CategoryId == cataegoryId)
                                      .ToListAsync();

            return Ok(weapons);
        }
        [HttpPost]
        public async Task<ActionResult> AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        //[HttpPost]
        

    }
}
