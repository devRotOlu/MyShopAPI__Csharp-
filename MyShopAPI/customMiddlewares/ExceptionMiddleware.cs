using MyShopAPI.Errors;
using Newtonsoft.Json;

namespace MyShopAPI.CustomMiddlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {

                HandleException(httpContext, ex);
            }
        }

        private void HandleException(HttpContext httpContext, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode,
            serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.")
            };

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.WriteAsync(JsonConvert.SerializeObject(new { message }));
        }
    }
}
