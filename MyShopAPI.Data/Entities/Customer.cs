using Microsoft.AspNetCore.Identity;

namespace MyShopAPI.Data.Entities
{
    public class Customer : IdentityUser
    {
        public CustomerDetails Details { get; set; } = null!;
        public ICollection<DeliveryProfile> DeliveryProfiles { get; set; } = null!;
        public ICollection<CustomerOrder> Orders { get; set; } = null!;
    }
}
