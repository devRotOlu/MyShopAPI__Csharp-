namespace MyShopAPI.Services.Models
{
    public class PayPalClient
    {
        public string Mode { get; set; }

        public string ClientSecret { get; set; }

        public string ClientId { get; set; }

        public string BaseUrl { get; set; }
    }
}
