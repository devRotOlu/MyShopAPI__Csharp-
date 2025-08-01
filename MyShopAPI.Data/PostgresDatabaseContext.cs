using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyShopAPI.Data.ApplicationDBContext;
using MyShopAPI.Data.Configuration;
using MyShopAPI.Data.Entities;
using OtherAttribute = MyShopAPI.Data.Entities.Attribute;

namespace MyShopAPI.Data
{
    public class PostgresDatabaseContext : IdentityDbContext<Customer>, IApplicationDbContext
    {
        public PostgresDatabaseContext(DbContextOptions options) : base(options) { }

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RolesConfiguration());

            builder.Entity<DeliveryProfile>()
               .HasMany(entity => entity.Orders)
               .WithOne(entity => entity.DeliveryProfile)
               .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<Customer>()
                .HasMany(entity => entity.Orders)
                .WithOne(entity => entity.Customer)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<Wishlist>(entity =>
            {
                entity.HasKey(list => new {list.CustomerId,list.ProductId});
                entity.Property(item => item.Id)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });
              

            builder.Entity<Cart>(entity =>
            {
                entity.HasKey(cart => new { cart.CustomerId, cart.ProductId });

                entity.ToTable(tb => tb.HasTrigger("SomeTrigger"))
                    .Property(cart => cart.Id)
                    .UseIdentityColumn()
                    .ValueGeneratedOnAdd()
                    .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });


            builder.Entity<ProductReview>()
                .ToTable(tb => tb.HasTrigger("SomeTrigger"))
                .HasKey(review => new { review.ReviewerId, review.ProductId});

            builder.Entity<CartOrder>()
                .HasKey(order => new { order.OrderId, order.ProductId, order.CustomerId });

            builder.Entity<ProductAttribute>()
                .HasKey(att => new { att.AttributeId, att.ProductId });
        }
    }

}
