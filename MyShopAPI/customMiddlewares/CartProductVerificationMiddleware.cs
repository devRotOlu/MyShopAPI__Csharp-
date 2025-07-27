using MyShopAPI.Core.DTOs.CartDTOs;
using MyShopAPI.Core.DTOs.ProductReviewDTOs;
using MyShopAPI.Data.Entities;
using System.Linq.Expressions;

namespace MyShopAPI.CustomMiddlewares
{
    public class CartProductVerificationMiddleware : VerificationMiddleware
    {
        private readonly List<string> paths;

        public CartProductVerificationMiddleware(RequestDelegate next) : base(next)
        {
            paths = new List<string>()
            {
                //"/api/Cart/add_item",
                "/api/Cart/update_item",
                "/api/Product/add-review",
            };
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethods.Options && paths.Contains(context.Request.Path))
            {
                var dataString = await ReadRequest(context);

                object _productId = null, _customerId = null;

                Func<Cart, bool> verifyItem = null;
                Expression<Func<Cart, bool>> query = null;

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

                //else if (context.Request.Path == "/api/Cart/add_item")
                //{
                //    (_customerId, _productId) = GetPropertyValue<AddCartDTO>(dataString, "CustomerId", "ProductId");

                //    var _unitOfWork = GetUnitOfWOrk(context);

                //    var cartItem = await _unitOfWork.Carts.Get(cart => cart.CustomerId == (string)_customerId && cart.ProductId == (int)_productId);

                //    if (cartItem?.Quantity == 0)
                //    {

                //        var data = JsonConvert.DeserializeObject<AddCartDTO>(dataString);

                //        context.Request.Path = "/api/Cart/update_item";

                //        context.Request.Method = "PUT";

                //        var updateDTO = new UpdateCartDTO
                //        {
                //            Id = cartItem.Id,
                //            CustomerId = cartItem.CustomerId,
                //            ProductId = cartItem.ProductId,
                //            Quantity = data!.Quantity,
                //        };

                //        var jsonObj = JsonConvert.SerializeObject(updateDTO);

                //        var stringContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");

                //        var stream = await stringContent.ReadAsStreamAsync();

                //        context.Request.Body = stream;

                //        context.Request.ContentLength = context.Request.Body.Length;

                //    }
                //    else if (cartItem?.Quantity > 0)
                //    {
                //        Func<bool, bool> _verifyItem = isTrue => isTrue == true;

                //        VerifyItem(_verifyItem, true);
                //    }
                //}
            }
            await _next.Invoke(context);
        }
    }
}
