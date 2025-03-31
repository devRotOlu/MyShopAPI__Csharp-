using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MyShopAPI.Services.Models;
using MyShopAPI.Services.ServiceOptions;

namespace MyShopAPI.Services.Email
{
    public class EmailService : IEmailService
    {
        const string templatePath = @"EmailTemplate/{0}.html";

        private readonly SMTPConfig _smtpConfig;

        public EmailService(IOptions<SMTPConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions, bool isEmailConfirmPage)
        {
            userEmailOptions.Subject = "Confirmation of email Id";

            var templateName = isEmailConfirmPage ? "EmailConfirmationPage" : "EmailTestPage";

            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody(templateName), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailForPasswordReset(UserEmailOptions userEmailOptions, bool isResetPasswordPage)
        {
            userEmailOptions.Subject = "Reset your password";

            var templateName = isResetPasswordPage ? "ForgetPasswordPage" : "ForgetPasswordTest";

            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody(templateName), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        private string UpdatePlaceholders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));

            return body;
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_smtpConfig.SenderName, _smtpConfig!.SenderEmail));
            emailMessage.Subject = userEmailOptions.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = userEmailOptions.Body };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                emailMessage.To.Add(new MailboxAddress("", toEmail));
            }

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_smtpConfig.SmtpServer, _smtpConfig.SmtpPort, _smtpConfig.UseSSL ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_smtpConfig.SenderEmail, _smtpConfig.Password);
                await smtp.SendAsync(emailMessage);
                await smtp.DisconnectAsync(true);
            } 
        }
    }
}
