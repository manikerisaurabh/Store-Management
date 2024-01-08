namespace RestaurantAPI.Models
{
    public class InventoryReceipts
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string SupplierName { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        
    }
}
