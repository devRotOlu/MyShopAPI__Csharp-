using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        [Required,ForeignKey("Customer")]
        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        public DateTime ExpirationTime { get; set; } 
    }
}
