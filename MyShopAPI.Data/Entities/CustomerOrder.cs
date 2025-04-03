using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace MyShopAPI.Data.Entities
{
    public class CustomerOrder
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string OrderId { get; set; } = GenerateOrderId();
        [Required]
        public DateOnly OrderDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [Required]
        public IEnumerable<Cart> ItemsOrdered { get; set; } = null!;
        [Required, ForeignKey("DeliveryProfile")]
        public int DeliveryProfileId { get; set; }
        public DeliveryProfile DeliveryProfile { get; set; } = null!;
        public string? OrderStatus { get; set; } = "Processing";
        [Required,ForeignKey("Customer")]
        public string CustomerId { get; set; } = null!;
        public Customer Customer { get; set; } = null!;

        private static string GenerateOrderId()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            using var rng = RandomNumberGenerator.Create();
   
            var now = DateTime.Now; 
            string dateTimeString = now.ToString("yy:mm:dd HH-mm-ss");

            return new string(Enumerable.Range(0, 5)
                .Select(_ => chars[GetRandomInt(rng, chars.Length)])
                .ToArray()) + "_" + dateTimeString;
        }

        private static int GetRandomInt(RandomNumberGenerator rng, int max)
        {
            byte[] bytes = new byte[4];
            rng.GetBytes(bytes);
            return (int)BitConverter.ToUInt32(bytes, 0) % max;
        }
    }
}
