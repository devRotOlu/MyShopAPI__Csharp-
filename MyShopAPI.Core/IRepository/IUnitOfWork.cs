using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<Customer> Customers { get; set; }
        IGenericRepository<Cart> Carts {  get; set; }
        IGenericRepository<Product> Products { get; set; }
        IGenericRepository<Image> Images { get; set; }
        IGenericRepository<ProductReview> ProductReviews { get; set; }
        Task Save();
    }
}
