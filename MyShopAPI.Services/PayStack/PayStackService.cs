using Microsoft.Extensions.Configuration;
using MyShopAPI.Services.Errors;
using PayStack.Net;

namespace MyShopAPI.Services.PayStack
{
    public class PayStackService : IPayStackService
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKey;
        private PayStackApi _payStackApi;

        public PayStackService(IConfiguration configuration)
        {
            _configuration = configuration;
            _secretKey = _configuration["Paystack:SecretKey"]!;
            _payStackApi = new PayStackApi(_secretKey);

        }

        public TransactionInitializeResponse InitializePayment(string email, decimal amount)
        {
            var payStackRequest = new TransactionInitializeRequest
            {
                AmountInKobo = (int)Math.Ceiling(amount * 14),
                Email = email,
                Currency = "NGN",
                CallbackUrl = _configuration["Paystack:CallbackUrl"],
                Channels = new string[] { "card" }
            };

            var response = _payStackApi.Transactions.Initialize(payStackRequest);

            if (!response.Status)
            {
                throw new PaymentAuthorizationException();
            }

            return response!;
        }

        public TransactionVerifyResponse VerifyPayment(string reference)
        {
            var verifyResponse = _payStackApi.Transactions.Verify(reference);

            if (!verifyResponse.Status)
            {
                throw new PaymentAuthorizationException();
            }

            return verifyResponse!;
        }
    }
}
