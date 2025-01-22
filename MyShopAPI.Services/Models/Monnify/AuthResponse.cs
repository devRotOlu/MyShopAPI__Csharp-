namespace MyShopAPI.Services.Models.Monnify
{
    public class AuthResponse
    {
        public bool RequestSuccessful { get; set; }
        public string ResponseMessage { get; set; } = null!;
        public string ResponseCode { get; set; } = null!;
        public AuthResponseBody ResponseBody { get; set; } = null!;
    }

    public class AuthResponseBody
    {
        public string AccessToken { get; set; } = null!;
        public int ExpiresIn { get; set; }
    }
}
