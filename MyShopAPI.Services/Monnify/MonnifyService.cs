using Microsoft.Extensions.Configuration;
using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Errors;
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

            var authResponse = await SendRequest<AuthResponse>(httpClient, "/api/v1/auth/login", null, "", HttpMethod.Post);

            if (!authResponse!.RequestSuccessful || authResponse.ResponseMessage.ToUpper() != "SUCCESS")
            {
                throw new PaymentAuthorizationException();
            }

            token = authResponse!.ResponseBody.AccessToken;
        }

        public async Task<InitialTransactionResponse> InitilaizeTransaction(string cutomerEmail,float amount)
        {
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

            var transactResponse = await SendRequest<InitialTransactionResponse>(httpClient, "/api/v1/merchant/transactions/init-transaction", Encoding.UTF8, jsonContent, HttpMethod.Post);

            return transactResponse!;
        }

        public async Task<BankTransferResponse> GetBankTransferInfo(string bankCode, string transactionReference)
        {
            var requestContent = new { transactionReference, bankCode };

            var httpClient = InitializeHTTPClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = JsonConvert.SerializeObject(requestContent);

            var result = await SendRequest<BankTransferResponse>(httpClient, "/api/v1/merchant/bank-transfer/init-payment", Encoding.UTF8, jsonContent, HttpMethod.Post);

            return result!;
        }

        public async Task<ChargeCardResponse> CardPayment(ChargeCardRequest chargeCard)
        {
            var httpClient = InitializeHTTPClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonContent = JsonConvert.SerializeObject(chargeCard);

            var result = await SendRequest<ChargeCardResponse>(httpClient, "/api/v1/merchant/cards/charge", Encoding.UTF8, jsonContent, HttpMethod.Post);

            return result!;
        }

        public async Task<TransactionStatus> GetTransactionStatus(string transactionRef)
        {
            var httpClient = InitializeHTTPClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await SendRequest<TransactionStatus>(httpClient, $"/api/v2/transactions/{transactionRef}", null, "",HttpMethod.Get);

            return result!;
        }

        private HttpClient InitializeHTTPClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_configuration["Monnify:BaseURI"]!);
            return httpClient;
        }

        private async Task<T?> SendRequest<T>(HttpClient httpClient, string url, Encoding? encoding, string requestContent,HttpMethod httpMethod) 
        {
            HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
            request.Content = new StringContent(requestContent, encoding, "application/json");

            var response = await httpClient.SendAsync(request);

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(json);

            httpClient.Dispose();

            return result;
        }
    }
}
