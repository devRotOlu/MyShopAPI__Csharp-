using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Customer> Customers { get; set; }
        IGenericRepository<CartAndWishlist> CartsAndWishlists {  get; set; }
        IGenericRepository<Product> Products { get; set; }
        IGenericRepository<ProductImage> ProductImages { get; set; }
        IGenericRepository<ProductReview> ProductReviews { get; set; }
        IGenericRepository<CustomerDetails> CustomerDetails { get; set; }
        IGenericRepository<RefreshToken> RefreshTokens { get; set; }
        Task Save();
    }
}
