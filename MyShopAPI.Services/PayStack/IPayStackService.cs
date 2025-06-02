using PayStack.Net;

namespace MyShopAPI.Services.PayStack
{
    public interface IPayStackService
    {
        public TransactionInitializeResponse InitializePayment(string email, decimal amount);
        public TransactionVerifyResponse VerifyPayment(string reference);
    }
}
