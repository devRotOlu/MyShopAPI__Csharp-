using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.UserDTOs
{
    public class SignUpDTO : LoginDTO
    {
        [Required, StringLength(100, ErrorMessage = "Enter a valid name.", MinimumLength = 1)]
        public string FirstName { get; set; } = null!;

        [Required, StringLength(100, ErrorMessage = "Enter a valid name.", MinimumLength = 1)]
        public string LastName { get; set; } = null!;

        [Required, DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
    }
}
