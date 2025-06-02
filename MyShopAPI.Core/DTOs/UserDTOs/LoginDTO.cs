using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.UserDTOs
{
    public class LoginDTO
    {
        [Required, DataType(DataType.EmailAddress, ErrorMessage = "Invalid email.")]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Password), StringLength(15, ErrorMessage = "Your password is longer than required")]
        public string Password { get; set; } = null!;
    }
}
