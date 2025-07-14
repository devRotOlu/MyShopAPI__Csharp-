using Microsoft.EntityFrameworkCore;
using MyShopAPI.Data.Entities;
using OtherAttribute = MyShopAPI.Data.Entities.Attribute;

namespace MyShopAPI.Data.ApplicationDBContext
{
    public interface IApplicationDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<CustomerDetails> CustomersDetails { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<DeliveryProfile> DeliveryProfiles { get; set; }
        public DbSet<CustomerOrder> Orders { get; set; }
        public DbSet<CartOrder> CartOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OtherAttribute> Attributes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        void Dispose();
    }
}
