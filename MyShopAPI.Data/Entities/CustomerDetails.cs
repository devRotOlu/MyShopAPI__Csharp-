using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    public class CustomerDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string PhoneNumber {  get; set; } = null!;
        public string? ShippingAddress { get; set; }
        public string? BillingAddress { get; set; } = null!;
        public string? ProfilePictureUrI { get; set; } = null!;
        public string? ProfilePicturePublicId { get; set; } = null!;
        [Required,ForeignKey("Customer")]
        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}
