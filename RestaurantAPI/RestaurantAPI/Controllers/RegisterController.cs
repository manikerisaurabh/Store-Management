using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Context;
using RestaurantAPI.Models;
using System.Formats.Asn1;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly DataContext _context;

        public RegisterController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegisterSuppliercs>>> getCharacters()
        {
            return await _context.Suppliers.ToListAsync();
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<IEnumerable<Categorycs>>> getCategoris()
        {
            return await _context.Categories.ToListAsync();
        }

        [HttpGet("getAllProducts")]

        public async Task<ActionResult<IEnumerable<Product>>> getAllProduct(int cataegoryId)
        {
            var weapons = await _context.Products
                                      .Where(w => w.CategoryId == cataegoryId)
                                      .ToListAsync();

            return Ok(weapons);
        }

        [HttpPost]
        public async Task<ActionResult> RegisterSupplier(RegisterSuppliercs supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();

            return Ok(supplier);

        }

        [HttpPost("addCategory")]
        public async Task<ActionResult> AddCategory(Categorycs categorycs)
        {
            _context.Categories.Add(categorycs);
            _context.SaveChanges();

            return Ok(categorycs);

        }
        [HttpPost("addProduct")]
        public async Task<ActionResult> AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        

        //add the stock in existing stock
        [HttpPost("addInventory")]
        public async Task<ActionResult> AddInventory(int catagoryId, int productId, int quantity, Inventory inventory)
        {
            var category = await _context.Categories.FindAsync(catagoryId);
            var product = await _context.Products.FindAsync(productId);

            inventory.CategoryName = category.Name; 
            inventory.ProductName = product.Name;
            inventory.ProductCost = (decimal)product.Cost;
            inventory.Quantity = quantity;

            var existingInventory = await _context.Inventory
                                          .FirstOrDefaultAsync(i => i.CategoryName == inventory.CategoryName &&
                                                                    i.ProductName == inventory.ProductName);

            if (existingInventory != null)
            {
                // If the record exists, update the Quantity and Total
                existingInventory.Quantity += quantity;
                existingInventory.Total = existingInventory.Quantity * existingInventory.ProductCost;
                existingInventory.ProductCost = (decimal)inventory.ProductCost;
            }
            else
            {
                // If the record doesn't exist, add a new Inventory record
                var total = inventory.Quantity * inventory.ProductCost;
                inventory.Total = total;
                _context.Inventory.Add(inventory);
            }


            //var total = inventory.Quantity * inventory.ProductCost;
            //inventory.Total = total;
            //_context.Inventory.Add(inventory);
            _context.SaveChanges();
            return Ok(inventory);
        }


        [HttpGet("getInventory")]
        public async Task<ActionResult<IEnumerable<Inventory>>> getInventoryData()
        {
            return await _context.Inventory.ToListAsync();
        }


        [HttpGet("getInventoryReceipt")]
        public async Task<ActionResult<IEnumerable<InventoryReceipts>>> getInventoryReceipt()
        {
            return await _context.InventoryReceipts.ToListAsync();
        }



        [HttpPost("addInventoryReciept")]
        public async Task<ActionResult> addInventoryReciept(int supplierId, int productId, InventoryReceipts inventoryReceipts)
        {
            var supplier = await _context.Suppliers.FindAsync(supplierId);
            var product = await _context.Products.FindAsync(productId);

            inventoryReceipts.Date = DateTime.Now;
            inventoryReceipts.SupplierName = supplier.Name;
            inventoryReceipts.ProductName = product.Name;
            inventoryReceipts.Total = ((decimal)(inventoryReceipts.Quantity * product.Cost));
            _context.InventoryReceipts.Add(inventoryReceipts);
            _context.SaveChanges();
            return Ok(inventoryReceipts);
        }

        [HttpGet("getTotalPurchesesToday")]
        public async Task<ActionResult<decimal>> getTotalPurchesesToday()
        {
            DateTime startOfToday = DateTime.Today;
            DateTime endOfToday = startOfToday.AddDays(1).AddTicks(-1);

            decimal totalSales = await _context.InventoryReceipts
                .Where(s => s.Date >= startOfToday && s.Date <= endOfToday)
                .SumAsync(s => s.Total);

            return Ok(totalSales);
        }
        [HttpGet("getTotalPurchesesLastWeek")]
        public async Task<ActionResult<decimal>> getTotalPurchesesLastWeek()
        {
            // Get the start and end of the last week
            DateTime today = DateTime.Today;
            DateTime startOfLastWeek = today.AddDays(-((int)today.DayOfWeek + 6) % 7-7); // Start of last week
            DateTime endOfLastWeek = startOfLastWeek.AddDays(7).AddTicks(-1); // End of last week is start of this week minus one tick

            // Query the database for total sales for last week
            decimal totalPurcheses = await _context.InventoryReceipts
                .Where(s => s.Date >= startOfLastWeek && s.Date <= endOfLastWeek)
                .SumAsync(s => s.Total);

            return Ok(totalPurcheses);
        }
        [HttpGet("getTotalPurchesesForMonth")]
        public async Task<ActionResult<decimal>> getTotalPurchesesForMonth()
        {
            DateTime today = DateTime.Today;
            DateTime startOfCurrentMonth = new DateTime(today.Year, today.Month, 1); // Start of the current month
            DateTime endOfCurrentMonth = startOfCurrentMonth.AddMonths(1).AddDays(-1); // End of the current month is the last day of the current month

            // Query the database for total sales for the current month
            decimal totalPurches = await _context.InventoryReceipts
                .Where(s => s.Date >= startOfCurrentMonth && s.Date <= endOfCurrentMonth)
                .SumAsync(s => s.Total);

            return Ok(totalPurches);
        }


        [HttpGet("GetTotalPurchesesCurrentYear")]
        public async Task<ActionResult<decimal>> GetTotalPurchesesCurrentYear()
        {
            // Get the start and end of the current year
            DateTime today = DateTime.Today;
            DateTime startOfCurrentYear = new DateTime(today.Year, 1, 1); // Start of the current year
            DateTime endOfCurrentYear = startOfCurrentYear.AddYears(1).AddTicks(-1); // End of the current year is start of next year minus one tick

            // Query the database for total sales for the current year
            decimal totalPurcheses = await _context.InventoryReceipts
                .Where(s => s.Date >= startOfCurrentYear && s.Date <= endOfCurrentYear)
                .SumAsync(s => s.Total);

            return Ok(totalPurcheses);
        }

        [HttpGet("getTotalPurchesCount")]
        public async Task<ActionResult<decimal>> getTotalPurchesCount()
        {
            decimal totalPurches = await _context.InventoryReceipts
                .SumAsync(r => r.Total); 
            return Ok(totalPurches);
        }

        [HttpGet("getAllSales")]
        public async Task<ActionResult<IEnumerable<Sales>>> getAllSales()
        {
            return await _context.Sales.ToListAsync();
        }

        [HttpPost("addNewOrder")]
        public async Task<ActionResult> addNewOrder(int productId, Sales sales)
        {
            var product = _context.Products.Find(productId);
            sales.Date = DateTime.Now;

            var existingInventory = await _context.Inventory
                                          .FirstOrDefaultAsync(i => 
                                                                    i.ProductName == product.Name);

            if(existingInventory != null)
            {
                existingInventory.Quantity -= sales.Quantity;
                existingInventory.Total = (decimal)(existingInventory.Quantity * product.Cost);
            }

            sales.ProductName = product.Name;
            sales.Total = ((decimal)(product.Cost * sales.Quantity));
            _context.Sales.Add(sales);
            _context.SaveChanges();
            return Ok(sales);
        }

        [HttpGet("getTotalSalesToday")]
        public async Task<ActionResult<decimal>> GetTotalSalesToday()
        {
            // Get the start and end of today
            DateTime startOfToday = DateTime.Today;
            DateTime endOfToday = startOfToday.AddDays(1).AddTicks(-1); // End of today is start of tomorrow minus one tick
            // Query the database for total sales for today
            decimal totalSales = await _context.Sales
                .Where(s => s.Date >= startOfToday && s.Date <= endOfToday)
                .SumAsync(s => s.Total);

            return Ok(totalSales);
        }

        [HttpGet("getTotalSalesLastWeek")]
        public async Task<ActionResult<decimal>> GetTotalSalesLastWeek()
        {
            // Get the start and end of the last week
            DateTime today = DateTime.Today;
            DateTime startOfLastWeek = today.AddDays(-((int)today.DayOfWeek + 6) % 7-7); // Start of last week
            DateTime endOfLastWeek = startOfLastWeek.AddDays(7).AddTicks(-1); // End of last week is start of this week minus one tick

            // Query the database for total sales for last week
            decimal totalSales = await _context.Sales
                .Where(s => s.Date >= startOfLastWeek && s.Date <= endOfLastWeek)
                .SumAsync(s => s.Total);

            return Ok(totalSales);
        }

        [HttpGet("getTotalSalesForMonth")]
        public async Task<ActionResult<decimal>> GetTotalSalesForMonth()
        {
            DateTime today = DateTime.Today;
            DateTime startOfCurrentMonth = new DateTime(today.Year, today.Month, 1); // Start of the current month
            DateTime endOfCurrentMonth = startOfCurrentMonth.AddMonths(1).AddDays(-1); // End of the current month is the last day of the current month

            // Query the database for total sales for the current month
            decimal totalSales = await _context.Sales
                .Where(s => s.Date >= startOfCurrentMonth && s.Date <= endOfCurrentMonth)
                .SumAsync(s => s.Total);

            return Ok(totalSales);
        }

        [HttpGet("GetTotalSalesCurrentYear")]
        public async Task<ActionResult<decimal>> GetTotalSalesCurrentYear()
        {
            // Get the start and end of the current year
            DateTime today = DateTime.Today;
            DateTime startOfCurrentYear = new DateTime(today.Year, 1, 1); // Start of the current year
            DateTime endOfCurrentYear = startOfCurrentYear.AddYears(1).AddTicks(-1); // End of the current year is start of next year minus one tick

            // Query the database for total sales for the current year
            decimal totalSales = await _context.Sales
                .Where(s => s.Date >= startOfCurrentYear && s.Date <= endOfCurrentYear)
                .SumAsync(s => s.Total);

            return Ok(totalSales);
        }


        [HttpGet("getTotalSaleCount")]
        public async Task<ActionResult<decimal>> getTotalSaleCount()
        {
            decimal totalSale = await _context.Sales
                .SumAsync (s => s.Total);
            return Ok(totalSale);
        }
    }
}
