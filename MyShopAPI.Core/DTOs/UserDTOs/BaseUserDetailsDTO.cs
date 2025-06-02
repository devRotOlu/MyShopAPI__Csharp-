using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.UserDTOs
{
    public class BaseUserDetailsDTO
    {
        [Required, StringLength(300, MinimumLength = 1)]
        public string FirstName { get; set; } = null!;
        [Required, StringLength(300, MinimumLength = 1)]
        public string LastName { get; set; } = null!;
    }
}
