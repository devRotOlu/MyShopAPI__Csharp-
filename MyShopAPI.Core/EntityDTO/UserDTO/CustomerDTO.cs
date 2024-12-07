namespace MyShopAPI.Core.EntityDTO.UserDTO
{
    public class CustomerDTO:ReviewerDTO
    {
        public string Id { get; set; } = null!;
        public string BillingAddress { get; set; } = null!;
        public string? ShippingAddress { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
