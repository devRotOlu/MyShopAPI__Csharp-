namespace MyShopAPI.Data.Entities
{
    public class Wishlist : ChoiceProduct
    {
        public bool isDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
