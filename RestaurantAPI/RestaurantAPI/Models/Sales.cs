using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Sales
    {
        [Key]
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string Contact { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
