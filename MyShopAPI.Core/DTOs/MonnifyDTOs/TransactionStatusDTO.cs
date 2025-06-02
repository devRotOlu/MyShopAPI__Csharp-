using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.MonnifyDTOs
{
    public class TransactionStatusDTO
    {
        [Required,MinLength(1)]
        public string TransactionReference { get; set; } = null!;
        [Required, Range(1, Double.MaxValue)]
        public int ProfileId { get; set; }
        public string? OrderInstruction { get; set; }
    }
}
