namespace MyShopAPI.Data.Entities
{
    public class CustomerDetails : BaseCustomer
    {
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
