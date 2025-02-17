using Microsoft.Extensions.Configuration;
using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Models.Monnify;
using MyShopAPI.Services.Models.Monnify.BankTransfer;
using MyShopAPI.Services.Models.Monnify.ChargeCard;
using MyShopAPI.Services.Models.Monnify.TransactionStatus;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MyShopAPI.Services.Monnify
{
    public class MonnifyService : IMonnifyService
    {
        private readonly IConfiguration _configuration;

        private string token = string.Empty;

        public MonnifyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Authorization()
        {
            var httpClient = InitializeHTTPClient();

            var keys = $"{_configuration["Monnify:APIKey"]}:{_configuration["Monnify:SecretKey"]}";
            var keysByte = Encoding.UTF8.GetBytes(keys);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", $"{Convert.ToBase64String(keysByte)}");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var json = string.Empty;

            try
            {
               json = await SendRequest(httpClient, "api/v1/auth/login", null, "", HttpMethod.Post);
            }
            catch (Exception)
            {

            }

            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(json);
            token = authResponse!.ResponseBody.AccessToken;
        }

        public async Task<string> InitilaizeTransaction(List<Cart> items, string cutomerEmail)
        {
            float amount = 0.00f;

            foreach (var item in items)
            {
                amount += (float)(item.Quantity * item.Product.UnitPrice * 1500);
            }

            var httpClient = InitializeHTTPClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var dateTime = DateTime.Now;

            var requestContent = new InitialTransactionRequest
            {
                customerEmail = cutomerEmail,
                contractCode = _configuration["Monnify:ContractCode"]!,
                amount = amount,
                paymentReference = $"{dateTime}__{Guid.NewGuid().ToString()}"
            };

            var jsonContent = JsonConvert.SerializeObject(requestContent);

            var json = await SendRequest(httpClient, "/api/v1/merchant/transactions/init-transaction", Encoding.UTF8, jsonContent, HttpMethod.Post);
            var transactResponse = JsonConvert.DeserializeObject<InitialTransactionResponse>(json);

            return transactResponse.ResponseBody.TransactionReference;
        }

        public async Task<BankTransferResponse> GetBankTransferInfo(string bankCode, string transactionReference)
        {
            var requestContent = new { transactionReference, bankCode };

            var httpClient = InitializeHTTPClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = JsonConvert.SerializeObject(requestContent);

            var json = await SendRequest(httpClient, "/api/v1/merchant/bank-transfer/init-payment", Encoding.UTF8, jsonContent, HttpMethod.Post);

            var result = JsonConvert.DeserializeObject<BankTransferResponse>(json);

            return result;
        }

        public async Task<ChargeCardResponse> CardPayment(ChargeCardRequest chargeCard)
        {
            var httpClient = InitializeHTTPClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = JsonConvert.SerializeObject(chargeCard);

            var json = await SendRequest(httpClient, "/api/v1/merchant/cards/charge", Encoding.UTF8, jsonContent,HttpMethod.Post);
            var result = JsonConvert.DeserializeObject<ChargeCardResponse>(json);

            return result;
        }

        public async Task<TransactionStatus> GetTransactionStatus(string transactionRef)
        {
            var httpClient = InitializeHTTPClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = await SendRequest(httpClient, $"/api/v2/transactions/{transactionRef}", null, "",HttpMethod.Get);
            var result = JsonConvert.DeserializeObject<TransactionStatus>(json);

            return result;
        }

        private HttpClient InitializeHTTPClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_configuration["Monnify:BaseURI"]!);
            return httpClient;
        }

        private async Task<string> SendRequest(HttpClient httpClient, string url, Encoding? encoding, string requestContent,HttpMethod httpMethod)
        {
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
            request.Content = new StringContent(requestContent, encoding, "application/json");

            var response = await httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            httpClient.Dispose();

            return json;
        }
    }
}
