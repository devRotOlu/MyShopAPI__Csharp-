namespace MyShopAPI.Services.Models.Monnify.BankTransfer
{
    public class BankTransferResponse
    {
        public bool RequestSuccessful { get; set; } 
        public string ResponseMessage { get; set; } = null!;
        public ResponseBody responseBody { get; set; } = null!;
    }

    public class ResponseBody
    {
        public string AccountNumber { get; set; } = null!;
        public string AccountName { get; set; } = null!;
        public string BankName { get; set; } = null!;
        public string AccountDurationSeconds { get; set; } = null!;
        public string USSDPayment { get; set; } = null!;
        public string ExpiresOn { get; set; } = null!;
        public float Amount { get; set; }
        public float Fee { get; set; }
        public float TotalPayable { get;set; }
    }
}
