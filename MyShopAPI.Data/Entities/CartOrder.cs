using System.ComponentModel.DataAnnotations.Schema;

namespace MyShopAPI.Data.Entities
{
    //[PrimaryKey(nameof(OrderId), nameof(ProductId), nameof(CustomerId))]
    public class CartOrder
    {
        [ForeignKey("CustomerOrder")]
        public int OrderId { get; set; }
        public CustomerOrder CustomerOrder { get; set; } = null!;

        public string CustomerId { get; set; } = null!;
        public int ProductId { get; set; }
        [ForeignKey(nameof(CustomerId) + "," + nameof(ProductId))]
        public Cart CartItem { get; set; } = null!;
        public int OrderedQuantity { get; set; }
        public string? OrderInstruction { get; set; }
    }
}
