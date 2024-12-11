using Microsoft.AspNetCore.Identity;

namespace MyShopAPI.Data.Entities
{
    public class Customer : IdentityUser
    {
        public CustomerDetails Details { get; set; } = null!;
        //[Required]
        //public string FirstName { get; set; } = null!;
        //[Required]
        //public string LastName { get; set; } = null!;
        //public string? ShippingAddress { get; set; } 
        //public string? BillingAddress { get; set; } = null!;
        //public string? ProfilePictureUrI { get; set; } = null!;
        //public string? ProfilePicturePublicId { get; set; } = null!;
    }
}
