namespace RestaurantAPI.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

    }
}
