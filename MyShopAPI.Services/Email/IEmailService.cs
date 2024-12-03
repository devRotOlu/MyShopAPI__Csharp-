using MyShopAPI.Services.ServiceOptions;

namespace MyShopAPI.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions, bool isEmailConfirmPage);
        Task SendEmailForPasswordReset(UserEmailOptions userEmailOptions, bool isResetPasswordPage);
    }
}
