using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.UserDTO
{
    public class SignUpDTO:LoginDTO
    {
        [Required,StringLength(100,ErrorMessage ="Enter a valid name.",MinimumLength =1)]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(100, ErrorMessage = "Enter a valid name.", MinimumLength = 1)]
        public string LastName { get; set; } = null!;

        [StringLength(300, ErrorMessage = "Shipping address shouldn't be more than 300 characters.")]
        public string ShippingAddress { get; set; } = null!;

        [Required, StringLength(300, ErrorMessage = "Enter a billing address of no more than 300 characters.", MinimumLength = 1)]
        public string BillingAddress { get; set; } = null!;


        [Required,DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public IEnumerable<string> Roles { get; set; } = null!;

        public IFormFile ProfilePicture { get; set; } = null!;
    }
}
