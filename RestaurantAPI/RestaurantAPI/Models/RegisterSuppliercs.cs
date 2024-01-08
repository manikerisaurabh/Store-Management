using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public class RegisterSuppliercs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrimaryContact { get; set; }
        public string SecondaryContact { get; set;}
        public string Address { get; set; }

    }
}
