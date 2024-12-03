using Microsoft.Extensions.Options;
using MyShopAPI.Services.Models;
using MyShopAPI.Services.ServiceOptions;
using System.Net.Mail;
using System.Net;
using System.Text;

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

            var templateName = isResetPasswordPage? "ForgetPasswordPage" : "ForgetPasswordTest";

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

            MailMessage mail = new MailMessage()
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig!.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML,
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            mail.BodyEncoding = Encoding.Default;

            try
            {
                await smtpClient.SendMailAsync(mail);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            smtpClient.Dispose();
        }
    }
}
