using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Data.Configuration;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Data
{
    public class DatabaseContext : IdentityDbContext<Customer>
    {

        public DatabaseContext(DbContextOptions options) :
            base(options)
        { }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<CustomerDetails> CustomersDetails { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DeliveryProfile> DeliveryProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RolesConfiguration());
        }
    }
}
