namespace MyShopAPI.Services.Models
{
    public class SMTPConfig
    {
        public string SenderAddress { get; set; } = null!;

        public string SenderDisplayName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Host { get; set; } =null!;

        public int Port { get; set; }

        public bool EnableSSL { get; set; }

        public bool UseDefaultCredentials { get; set; }

        public bool IsBodyHTML { get; set; }
    }
}
