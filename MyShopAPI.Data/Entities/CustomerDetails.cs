namespace MyShopAPI.Data.Entities
{
    public class CustomerDetails : BaseCustomer
    {
        //[Required]
        //public string PhoneNumber {  get; set; } = null!;
        //public string? ShippingAddress { get; set; }
        //public string? BillingAddress { get; set; } = null!;
        //public string? ProfilePictureU rI { get; set; } = null!;
        //public string? ProfilePicturePublicId { get; set; } = null!;
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
