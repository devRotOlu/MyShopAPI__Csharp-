namespace MyShopAPI.Services.Models.Monnify.TransactionStatus
{
    public class TransactionStatus
    {
        public bool RequestSuccessful { get; set; }
        public string ResponseMessage { get; set; } = null!;
        public ResponseBody ResponseBody { get; set; } = null!;
    }

    public class ResponseBody
    {
        public string PaymentStatus { get; set; } = null!;
    }
}
