using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Data.Entities
{
    public class Customer : IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        public string? ShippingAddress { get; set; } 
        public string BillingAddress { get; set; } = null!;
        public string? ProfilePictureUrI { get; set; } = null!;
        public string? ProfilePicturePublicId { get; set; } = null!;
    }
}
