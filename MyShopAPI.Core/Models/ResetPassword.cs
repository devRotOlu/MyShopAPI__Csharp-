using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.Models
{
    public class ResetPassword
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;

        [Required, DataType(DataType.Password), StringLength(15, ErrorMessage = "Your password is longer than required")]
        public string NewPassword { get; set; } = null!;

        [Required, DataType(DataType.Password), Compare("NewPassword")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
