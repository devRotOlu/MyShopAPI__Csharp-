namespace MyShopAPI.Services.Models.Monnify
{
    public class InitialTransactionRequest
    {
       public float amount { get; set; }
       public string customerEmail { get; set; } = null!;
       public string paymentReference { get; set; } = null!;
       public string currencyCode { get; set; } = "NGN";
       public string contractCode { get; set; } = null!;
    }
}
