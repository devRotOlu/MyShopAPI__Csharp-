using System.Net;

namespace MyShopAPI.Errors
{
    public interface IServiceException
    {
        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }
    }
}
