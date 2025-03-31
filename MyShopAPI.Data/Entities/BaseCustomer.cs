using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Data.Entities
{
    public class BaseCustomer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required, ForeignKey("Customer")]
        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}
