namespace MyShopAPI.Services.Models.Monnify
{
    public class InitialTransactionResponse
    {
        public bool RequestSuccessful { get; set; }
        public string ResponseMessage { get; set; } = null!;
        public ResponseBody ResponseBody { get; set; } = null!;
    }

    public class ResponseBody
    {
        public string TransactionReference { get; set; } = null!;
    }
}
