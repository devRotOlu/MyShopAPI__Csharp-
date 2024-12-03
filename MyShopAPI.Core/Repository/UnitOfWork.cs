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
        public IGenericRepository<Image> Images { get; set; }
        public IGenericRepository<ProductReview> ProductReviews { get; set; }

        public UnitOfWork(DatabaseContext databaseContext)
        {
            Customers = new GenericRepository<Customer>(databaseContext);
            Carts = new GenericRepository<Cart>(databaseContext);
            Products = new GenericRepository<Product>(databaseContext);
            Images = new GenericRepository<Image>(databaseContext);
            ProductReviews = new GenericRepository<ProductReview>(databaseContext);
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
