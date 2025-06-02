using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.PaystackDTOs
{
    public class PaymentVerificationDTO
    {
        [Required,MinLength(1)]
        public string Reference { get; set; } = null!;
        [Required, Range(1, Double.MaxValue)]
        public int ProfileId { get; set; }
        public string? OrderInstruction { get; set; }
    }
}
