using MyShopAPI.Core.IRepository;
using Newtonsoft.Json;
using System.Net;

namespace MyShopAPI.CustomMiddlewares
{
    public class VerificationMiddleware
    {
        public RequestDelegate _next;

        public VerificationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public object GetPropertyValue<T>(string jsonData, string property) where T : class
        {
            var (dataType, data) = GetDataType<T>(jsonData);

            var propertyInfo = dataType.GetProperty(property);

            return propertyInfo!.GetValue(data)!;
        }

        public Tuple<object, object> GetPropertyValue<T>(string jsonData, string property1, string property2) where T : class
        {
            var (dataType, data) = GetDataType<T>(jsonData);

            var property1Info = dataType.GetProperty(property1);

            var value1 = property1Info!.GetValue(data)!;

            var property2Info = dataType.GetProperty(property2);

            var value2 = property2Info!.GetValue(data)!;

            return Tuple.Create(value1, value2);
        }

        public Tuple<object, object, object> GetPropertyValue<T>(string jsonData, string property1, string property2, string property3) where T : class
        {
            var (dataType, data) = GetDataType<T>(jsonData);

            var property1Info = dataType.GetProperty(property1);

            var value1 = property1Info!.GetValue(data)!;

            var property2Info = dataType.GetProperty(property2);

            var value2 = property2Info!.GetValue(data)!;

            var property3Info = dataType.GetProperty(property3);

            var value3 = property3Info!.GetValue(data)!;

            return Tuple.Create(value1, value2, value3);
        }


        public async Task<string> ReadRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            StreamReader reader = new StreamReader(context.Request.Body);
            var dataString = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            return dataString;
        }

        public string GetQueryParam(HttpContext context, string param)
        {
            context.Request.EnableBuffering();
            return context.Request.Query[param]!;
        }

        public IUnitOfWork GetUnitOfWOrk(HttpContext context)
        {
            var scope = context.RequestServices.CreateScope();
            var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            return _unitOfWork;
        }

        public void VerifyItem<T>(Func<T, bool> exp, T data)
        {
            if (exp.Invoke(data))
            {
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

                throw new Exception(response.ToString());
            }
        }

        private Tuple<Type, T> GetDataType<T>(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<T>(jsonData);

            return Tuple.Create(data!.GetType(), data);
        }
    }
}
