using Microsoft.AspNetCore.Identity;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Core.EmailMananger
{
    public interface IEmailManager
    {
        Task SendEmailConfirmationEmail(Customer user, string token, string userName, string? emailConfirmationLink = null);
        Task SendForgotPasswordEmail(Customer user, string token, string memberFirstName, string? passwordResetLink = null);
    }
}
