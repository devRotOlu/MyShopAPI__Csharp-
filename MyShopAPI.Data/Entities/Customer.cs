using Microsoft.AspNetCore.Identity;

namespace MyShopAPI.Data.Entities
{
    public class Customer : IdentityUser
    {
        public CustomerDetails Details { get; set; } = null!;
    }
}
