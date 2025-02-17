using MyShopAPI.Core.EntityDTO.CartDTO;
using MyShopAPI.Core.EntityDTO.ProductReviewDTO;
using MyShopAPI.Core.EntityDTO.WishlistDTO;

namespace MyShopAPI.customMiddlewares
{
    public class ProductVerificationMiddleware : VerificationMiddleware
    {
        private readonly List<string> paths;

        public ProductVerificationMiddleware(RequestDelegate next) : base(next)
        {
            paths = new List<string>()
            {
                "/api/wishlist/add_item",
                "/api/Cart/add_item",
                "/api/Product/add-review",
                "/api/Product/list-reviews"
            };
        }

        public async Task Invoke(HttpContext context)
        {
            if (paths.Contains(context.Request.Path))
            {

                var productId = 0;

                if (context.Request.Path == "/api/Product/list-reviews")
                {
                    productId = Convert.ToInt32(GetQueryParam(context, "productId"));
                }
                else
                {
                    var dataString = await ReadRequest(context);

                    if (context.Request.Path == "/api/wishlist/add_item")
                        productId = (int)GetPropertyValue<AddWishlistDTO>(dataString, "ProductId");

                    if (context.Request.Path == "/api/Cart/add_item")
                        productId = (int)GetPropertyValue<AddCartDTO>(dataString, "ProductId");

                    if (context.Request.Path == "/api/Product/add-review")
                        productId = (int)GetPropertyValue<AddReviewDTO>(dataString, "ProductId");
                }

                var _unitOfWork = GetUnitOfWOrk(context);

                var product = await _unitOfWork.Products.Get(product => product.Id == productId && product.Quantity > 0);

                VerifyItem(product => product == null, product);
            }

            await _next.Invoke(context);
        }
    }
}
