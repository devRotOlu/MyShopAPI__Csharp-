using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyShopAPI.Core.EntityDTO.UserDTO;
using MyShopAPI.Core.Models;
using MyShopAPI.Data.Entities;
using System.Security.Claims;

namespace MyShopAPI.Core.AuthManager
{
    public interface IAuthManager
    {
        Customer User { get; }
        Task<SignInResult> SignInUser(LoginDTO userDTO);
        Task<string> CreateToken();
        Task<Customer?> GetUserByEmailAsync(string email);
        Task GenerateEmailConfirmationTokenAsync(Customer user, string memberFirstName, string? emailConfirmationLink = null);
        Task GenerateForgotPasswordTokenAsync(Customer user, string memberFirstName, string? passwordResetLink = null);
        Task<IdentityResult> ResetPasswordAsync(ResetPassword model);
        Task<IdentityResult> ChangePasswordAsync(Customer user, string currentPassword, string newPassword);
        Task<IdentityResult> CreateAsync(Customer appUser, string password);
        Task<IdentityResult> AddToRolesAsync(Customer appUser, IEnumerable<string> roles);
        Task<Customer?> GetUserByPrincipalClaimsAsync(ClaimsPrincipal user);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task<Customer?> GetUserByIdAsync(string id);
        string GenerateRefreshToken();
        void SetTokenInCookies(Tokens tokens,HttpContext context);
        Task<TokenValidationResult> ValidateToken(string accessToken);
        Task<IdentityResult> DeleteAccount(Customer user);
    }
}
