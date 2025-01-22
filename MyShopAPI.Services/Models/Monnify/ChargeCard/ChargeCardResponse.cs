namespace MyShopAPI.Services.Models.Monnify.ChargeCard
{
    public class ChargeCardResponse
    {
        public bool RequestSuccessful { get; set; }
        public string ResponseMessage { get; set; } = null!;
        public ResponseBody ResponseBody { get; set; } = null!;
    }

    public class ResponseBody
    {
        public string Status { get; set; } = null!;
        public string Message { get; set; } = null!;
        public float AuthorizedAmount { get; set; }
    }
}
