using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Core.DTOs
{
    public class TokenDTO
    {
        [Required,MinLength(1)]
        public string CustomerId { get; set; } = null!;
        [Required,MinLength(1)]
        public string RefreshToken { get; set; } = null!;
    }
}
