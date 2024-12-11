using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.UserDTO
{
    public class DetailsDTO : BaseUserDetailsDTO
    {
        public string? BillingAddress { get; set; } 
        public string? ShippingAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = null!;
        public IFormFile? ProfilePicture { get; set; } = null!;
    }
}
