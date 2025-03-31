﻿namespace MyShopAPI.Services.Models
{
    public class SMTPConfig
    {
        public string SenderEmail { get; set; } = null!;

        public string SenderName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string SmtpServer { get; set; } = null!;

        public int SmtpPort { get; set; }

        public bool UseSSL { get; set; }
    }
}
