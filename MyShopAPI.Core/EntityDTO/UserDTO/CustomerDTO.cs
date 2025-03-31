namespace MyShopAPI.Core.EntityDTO.UserDTO
{
    public class CustomerDTO: BaseUserDetailsDTO
    {
        public string Id { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
