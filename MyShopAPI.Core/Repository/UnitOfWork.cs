using MyShopAPI.Core.IRepository;
using MyShopAPI.Data;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;
        public IGenericRepository<Customer> Customers { get; set; }
        public IGenericRepository<Product> Products { get; set; }
        public IGenericRepository<ProductImage> ProductImages { get; set; }
        public IGenericRepository<ProductReview> ProductReviews { get; set; }
        public IGenericRepository<CustomerDetails> CustomerDetails { get; set; }
        public IGenericRepository<RefreshToken> RefreshTokens { get; set; }
        public IGenericRepository<Cart> Carts { get; set; }
        public IGenericRepository<Wishlist> Wishlists { get; set; }
        public IGenericRepository<DeliveryProfile> DeliveryProfiles { get; set; }
        public IGenericRepository<CustomerOrder> Orders { get; set; }

        public UnitOfWork(DatabaseContext databaseContext)
        {
            Customers = new GenericRepository<Customer>(databaseContext);
            Carts = new GenericRepository<Cart>(databaseContext);
            Wishlists = new GenericRepository<Wishlist>(databaseContext);
            Products = new GenericRepository<Product>(databaseContext);
            ProductImages = new GenericRepository<ProductImage>(databaseContext);
            ProductReviews = new GenericRepository<ProductReview>(databaseContext);
            CustomerDetails = new GenericRepository<CustomerDetails>(databaseContext);
            RefreshTokens = new GenericRepository<RefreshToken>(databaseContext);
            DeliveryProfiles = new GenericRepository<DeliveryProfile>(databaseContext);
            Orders = new GenericRepository<CustomerOrder>(databaseContext);
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
