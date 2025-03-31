using MyShopAPI.Errors;
using System.Net;

namespace MyShopAPI.Services.Errors
{
    public class PaymentAuthorizationException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

        public string ErrorMessage => "Payment authorization failed";
    }
}
