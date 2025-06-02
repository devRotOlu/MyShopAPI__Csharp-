using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.UserDTOs
{
    public class CustomerDetailsDTO:BaseUserDetailsDTO
    {
        [Required, StringLength(300, MinimumLength = 1)]
        public string StreetAddress { get; set; } = null!;
        [Required, StringLength(200, MinimumLength = 1)]
        public string City { get; set; } = null!;
        [Required, StringLength(200, MinimumLength = 1)]
        public string State { get; set; } = null!;
        [StringLength(200, MinimumLength = 1)]
        public string? NewPassword { get; set; }
        [StringLength(200, MinimumLength = 1)]
        public string? CurrentPassword { get; set; }
    }
}
