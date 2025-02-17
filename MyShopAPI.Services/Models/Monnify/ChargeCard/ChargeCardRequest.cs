namespace MyShopAPI.Services.Models.Monnify.ChargeCard
{
    public class ChargeCardRequest
    {
        public string transactionReference { get; set; } = null!;
        //public string collectionChannel { get; set; } = null!;//"API_NOTIFICATION";
        public CardDetails card { get; set; } = null!;
    }

    public class CardDetails
    {
        public string number { get; set; } = null!;
        public string expiryMonth { get; set; } = null!;
        public string expiryYear { get; set; } = null!;
        public string pin { get; set; } = null!;
        public string cvv { get; set; } = null!;
    }
}
