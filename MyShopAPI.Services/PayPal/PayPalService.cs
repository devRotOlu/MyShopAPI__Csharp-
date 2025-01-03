using MyShopAPI.Data.Entities;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Authentication;
using PaypalServerSdk.Standard.Controllers;
using PaypalServerSdk.Standard.Http.Response;
using PaypalServerSdk.Standard.Models;
using MsIConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MyShopAPI.Services.PayPal
{
    public class PayPalService : IPayPalService
    {
        private readonly PaypalServerSdkClient _payPalClient;
        private readonly MsIConfiguration _configuration;

        public PayPalService(MsIConfiguration configuration)
        {
            _configuration = configuration;
            _payPalClient = new PaypalServerSdkClient.Builder()
                            .ClientCredentialsAuth(
                            new ClientCredentialsAuthModel.Builder(
                                     _configuration["PayPal:ClientID"],
                                     _configuration["PayPal:SecretKey"]
                                        )
                            .Build())
                            .Environment(PaypalServerSdk.Standard.Environment.Sandbox)
                            .Build();
        }

        public async Task<ApiResponse<Order>> CreateOrder(List<CartAndWishlist> items)
        {
            var purchaseUnits = new List<PurchaseUnitRequest>();

            for (int i = 0; i < items.Count(); i++)
            {
                var item = items[i];
                purchaseUnits.Add(
                    new PurchaseUnitRequest
                    {
                        Amount = new AmountWithBreakdown
                        {
                            CurrencyCode = "USD",
                            MValue = $"{(int)(item.Quantity * item.Product.UnitPrice)}",
                        },
                        ReferenceId = Guid.NewGuid().ToString(),
                    }
                    );
            }

            OrdersCreateInput orders = new OrdersCreateInput
            {
                Body = new OrderRequest
                {
                    Intent = CheckoutPaymentIntent.Capture,
                    PurchaseUnits = purchaseUnits
                }
            };
            OrdersController ordersController = _payPalClient.OrdersController;
            ApiResponse<Order> result = await ordersController.OrdersCreateAsync(orders);
            return result;
        }
    }
}
