using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.EntityDTO.UserDTO
{
    public class AddDeliveryProfileDTO:BaseUserDetailsDTO
    {
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string StreetAddress { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required]
        public string LGA { get; set; } = null!;
        public string? Directions { get; set; }
        public string? AdditionalInformation { get; set; }
    }
}
