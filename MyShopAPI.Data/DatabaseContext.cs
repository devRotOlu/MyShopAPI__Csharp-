using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyShopAPI.Data.Configuration;
using MyShopAPI.Data.Entities;
using OtherAttribute = MyShopAPI.Data.Entities.Attribute;

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
        public DbSet<CustomerOrder> Orders { get; set; }
        public DbSet<CartOrder> CartOrders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OtherAttribute> Attributes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RolesConfiguration());

            builder.Entity<ProductReview>()
                .ToTable(tb => tb.HasTrigger("SomeTrigger"));

            builder.Entity<DeliveryProfile>()
                .HasMany(entity => entity.Orders)
                .WithOne(entity => entity.DeliveryProfile)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<Customer>()
                .HasMany(entity => entity.Orders)
                .WithOne(entity => entity.Customer)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder.Entity<Cart>()
                .ToTable(tb=>tb.HasTrigger("SomeTrigger"))
                .Property(cart => cart.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

            builder.Entity<Wishlist>()
               .Property(item => item.Id)
               .UseIdentityColumn()
               .ValueGeneratedOnAdd()
               .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore); 
                   
        }
    }
}
