using MyShopAPI.Core.IRepository;
using MyShopAPI.Data;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        public IGenericRepository<Customer> Customers { get; set; }
        public IGenericRepository<Cart> Carts { get; set; }
        public IGenericRepository<Product> Products { get; set; }
        public IGenericRepository<ProductImage> ProductImages { get; set; }
        public IGenericRepository<ProductReview> ProductReviews { get; set; }
        public IGenericRepository<RefreshToken> RefreshTokens { get; set; }

        public UnitOfWork(DatabaseContext databaseContext)
        {
            Customers = new GenericRepository<Customer>(databaseContext);
            Carts = new GenericRepository<Cart>(databaseContext);
            Products = new GenericRepository<Product>(databaseContext);
            ProductImages = new GenericRepository<ProductImage>(databaseContext);
            ProductReviews = new GenericRepository<ProductReview>(databaseContext);
            RefreshTokens = new GenericRepository<RefreshToken>(databaseContext);
            _databaseContext = databaseContext;
        }

        public void Dispose()
        {
            _databaseContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _databaseContext.SaveChangesAsync();
        }
    }
}
