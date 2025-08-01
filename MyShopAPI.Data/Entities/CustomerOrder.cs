using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    public class CustomerOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OrderId { get; set; } = GenerateOrderId();
        [Required]
        public DateTime OrderDate { get; set; } 
        public IEnumerable<CartOrder> CartItems { get; set; } = null!;
        [Required, ForeignKey("DeliveryProfile")]
        public int DeliveryProfileId { get; set; }
        public DeliveryProfile DeliveryProfile { get; set; } = null!;
        public string? OrderStatus { get; set; } = "Processing";
        [Required, ForeignKey("Customer")]
        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
        //public ICollection<ProductReview> Reviews { get; set; } = null!;

        private static string GenerateOrderId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            var randomString = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            var dateString = DateTime.Now.ToString("yyMMddHHmmss");
            return randomString + dateString;
        }
    }
}
