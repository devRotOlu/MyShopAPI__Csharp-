using MyShopAPI.Core.EntityDTO.CartDTO;
using MyShopAPI.Core.EntityDTO.ProductReviewDTO;
using MyShopAPI.Data.Entities;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Text;

namespace MyShopAPI.customMiddlewares
{
    public class CartProductVerificationMiddleware : VerificationMiddleware
    {
        private readonly List<string> paths;

        public CartProductVerificationMiddleware(RequestDelegate next) : base(next)
        {
            paths = new List<string>()
            {
                "/api/Cart/add_item",
                "/api/Cart/update_item",
                "/api/Product/add-review",
            };
        }

        public async Task Invoke(HttpContext context)
        {
            if (paths.Contains(context.Request.Path))
            {
                var dataString = await ReadRequest(context);

                object _productId = null, _customerId = null;

                Func<Cart, bool> verifyItem = null;
                Expression<Func<Cart, bool>> query = null;

                if (context.Request.Path == "/api/Cart/update_item" || context.Request.Path == "/api/Product/add-review")
                {
                    if (context.Request.Path == "/api/Cart/update_item")
                    {
                        (_customerId, _productId) = GetPropertyValue<UpdateCartDTO>(dataString, "CustomerId", "ProductId");
                        verifyItem = cartItem => cartItem == null;
                        query = cart => cart.CustomerId == (string)_customerId && cart.ProductId == (int)_productId;
                    }

                    if (context.Request.Path == "/api/Product/add-review")
                    {
                        (_customerId, _productId) = GetPropertyValue<AddReviewDTO>(dataString, "ReviewerId", "ProductId");
                        query = cart => cart.ProductId == (int)_productId && cart.CustomerId == (string)_customerId && cart.IsPurchased != 0;
                        verifyItem = cartItem => cartItem == null;
                    }

                    var _unitOfWork = GetUnitOfWOrk(context);

                    var cartItem = await _unitOfWork.Carts.Get(query);

                    VerifyItem(verifyItem!, cartItem);

                }
                else if (context.Request.Path == "/api/Cart/add_item")
                {
                    (_customerId, _productId) = GetPropertyValue<AddCartDTO>(dataString, "CustomerId", "ProductId");

                    var _unitOfWork = GetUnitOfWOrk(context);

                    var cartItem = await _unitOfWork.Carts.Get(cart => cart.CustomerId == (string)_customerId && cart.ProductId == (int)_productId);

                    if (cartItem != null)
                    {
                        context.Request.Path = "/api/Cart/update_item";

                        context.Request.Method = "PATCH";

                        var updateDTO = new UpdateCartDTO
                        {
                            Id = cartItem.Id,
                            CustomerId = cartItem.CustomerId,
                            ProductId = cartItem.ProductId,
                            Quantity = cartItem.Quantity,
                        };

                        var jsonObj = JsonConvert.SerializeObject(updateDTO);

                        var requestContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                        var stream = await requestContent.ReadAsStreamAsync();

                        context.Request.Body = stream;

                        context.Request.ContentLength = context.Request.Body.Length;

                    }
                }
            }
            await _next.Invoke(context);
        }
    }
}
