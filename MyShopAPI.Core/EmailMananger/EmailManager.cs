using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Email;
using MyShopAPI.Services.ServiceOptions;

namespace MyShopAPI.Core.EmailMananger
{
    public class EmailManager : IEmailManager
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public EmailManager(UserManager<Customer> userManager, IConfiguration configuration, IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }
        public async Task SendEmailConfirmationEmail(Customer user, string token, string userName, string? emailConfirmationLink = null)
        {
            string? appName = _configuration["AppName"];
            string userId = "?id={0}&token={1}";

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>()
                {
                    user.Email!
                },

                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",userName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(emailConfirmationLink + userId,user.Id,token)),
                    new KeyValuePair<string, string>("{{AppName}}",appName!),
                    new KeyValuePair<string, string>("{{Token}}",token),
                    new KeyValuePair<string, string>("{{UserId}}",user.Id)
                }
            };

            var isEmailConfirmPage = !string.IsNullOrEmpty(emailConfirmationLink);

            await _emailService.SendEmailForEmailConfirmation(options, isEmailConfirmPage);
        }



        public async Task SendForgotPasswordEmail(Customer user, string token, string memberFirstName, string? passwordResetLink = null)
        {
            string userId = "?id={0}&token={1}";

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>()
                {
                    user.Email!
                },

                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",memberFirstName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(passwordResetLink+ userId,user.Id,token)),
                    new KeyValuePair<string, string>("{{Token}}",token),
                    new KeyValuePair<string, string>("{{UserID}}",user.Id),
                }
            };


            var isResetPasswordPage = !string.IsNullOrEmpty(passwordResetLink);

            await _emailService.SendEmailForPasswordReset(options, isResetPasswordPage);
        }
    }
}
