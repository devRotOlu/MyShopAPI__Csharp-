using System.ComponentModel.DataAnnotations;

namespace MyShopAPI.Services.Models.Monnify.ChargeCard
{
    public class ChargeCardRequest
    {
        public string transactionReference { get; set; } = null!;
        public CardDetails card { get; set; } = null!;
    }

    public class CardDetails
    {
        [CreditCard]
        public string number { get; set; } = null!;
        public string expiryMonth { get; set; } = null!;
        public string expiryYear { get; set; } = null!;
        public string pin { get; set; } = null!;
        public string cvv { get; set; } = null!;
    }
}
