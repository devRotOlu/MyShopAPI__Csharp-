using MyShopAPI.Services.Models.Monnify.ChargeCard;
using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs.MonnifyDTOs
{
    public class CardPaymentDTO
    {
        [Required]
        public ChargeCardRequest CardDetails { get; set; } = null!;
        [Required,Range(1,Double.MaxValue)]
        public int profileId { get; set; }
        public string? OrderInstruction { get; set; }
    }
}
