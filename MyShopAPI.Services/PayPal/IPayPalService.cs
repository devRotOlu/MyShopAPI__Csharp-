using MyShopAPI.Data.Entities;
using PaypalServerSdk.Standard.Http.Response;
using PaypalServerSdk.Standard.Models;

namespace MyShopAPI.Services.PayPal
{
    public interface IPayPalService
    {
        Task<ApiResponse<Order>> CreateOrder(List<CartAndWishlist> items);
        Task<ApiResponse<Order>> CaptureOrder(OrdersCaptureInput input);
        Task<ApiResponse<Order>> GetOrder(string orderId);
    }
}
