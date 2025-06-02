using MyShopAPI.Data.Entities;
using OtherAttribute = MyShopAPI.Data.Entities.Attribute;

namespace MyShopAPI.Core.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Customer> Customers { get; set; }
        IGenericRepository<Cart> Carts {  get; set; }
        IGenericRepository<Wishlist> Wishlists { get; set; }
        IGenericRepository<Product> Products { get; set; }
        IGenericRepository<ProductImage> ProductImages { get; set; }
        IGenericRepository<ProductReview> ProductReviews { get; set; }
        IGenericRepository<CustomerDetails> CustomerDetails { get; set; }
        IGenericRepository<RefreshToken> RefreshTokens { get; set; }
        IGenericRepository<DeliveryProfile> DeliveryProfiles { get; set; }
        IGenericRepository<CustomerOrder> Orders { get; set; }
        IGenericRepository<Category> Categories { get; set; }
        IGenericRepository<OtherAttribute> Attributes {get;set;}
        IGenericRepository<ProductAttribute> ProductAttributes { get; set; }

        Task Save();
    }
}
