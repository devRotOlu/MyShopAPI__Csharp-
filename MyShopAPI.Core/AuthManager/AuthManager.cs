using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyShopAPI.Core.DTOs.UserDTOs;
using MyShopAPI.Core.EmailMananger;
using MyShopAPI.Core.Models;
using MyShopAPI.Data.Entities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JwtRegNamesCalims = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace MyShopAPI.Core.AuthManager
{
    public class AuthManager : IAuthManager
    {

        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IEmailManager _emailManager;


        public AuthManager(UserManager<Customer> userManager, IConfiguration configuration, SignInManager<Customer> signInManager, IEmailManager emailManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailManager = emailManager;
        }

        public async Task<IdentityResult> AddToRolesAsync(Customer appUser, IEnumerable<string> roles)
        {

            return await _userManager.AddToRolesAsync(appUser, roles);
        }

        public async Task<bool> ConfirmEmailAsync(string uid, string token)
        {
            var user = await _userManager.FindByIdAsync(uid);

            if (user == null || user.EmailConfirmed) return false;

            var decodedToken = Base64UrlEncoder.Decode(token);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            return result.Succeeded;
        }

        public async Task<IdentityResult> CreateAsync(Customer appUser, string password)
        {
            return await _userManager.CreateAsync(appUser, password);
        }

        public async Task<string> CreateToken(string email)
        {
            var signingCredentials = GetSigningCredentials();

            var claims = await GetClaims(email);

            var tokenDescriptor = GenerateTokenOptions(signingCredentials, claims);

            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }

        public async Task<IdentityResult> ChangePasswordAsync(Customer user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }


        public async Task GenerateEmailConfirmationTokenAsync(Customer user, string memberFirstName, string? emailConfirmationLink = null)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = Base64UrlEncoder.Encode(token);

            if (!string.IsNullOrEmpty(token))
            {
                await _emailManager.SendEmailConfirmationEmail(user, encodedToken, memberFirstName, emailConfirmationLink);
            }
        }

        public async Task GenerateForgotPasswordTokenAsync(Customer user, string memberFirstName, string? passwordResetLink = null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = Base64UrlEncoder.Encode(token);

            if (!string.IsNullOrEmpty(token))
            {
                await _emailManager.SendForgotPasswordEmail(user, encodedToken, memberFirstName, passwordResetLink);
            }
        }

        public async Task<Customer?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


        public async Task<Customer?> GetUserByPrincipalClaimsAsync(ClaimsPrincipal user)
        {
            return await _userManager.GetUserAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPassword model)
        {
            var decodedToken = Base64UrlEncoder.Decode(model.Token);
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), decodedToken, model.NewPassword);
        }

        public async Task<SignInResult> SignInUser(LoginDTO userDTO)
        {

            return await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, false);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration.GetValue<string>("Jwt:key");

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IDictionary<string, object>> GetClaims(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = new Dictionary<string, object>();
            claims.Add(ClaimTypes.Name, user!.UserName!);
            claims.Add(JwtRegNamesCalims.Aud, _configuration.GetValue<string>("Jwt:Issuer")!);

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(ClaimTypes.Role, role);
            }

            return claims;
        }

        private SecurityTokenDescriptor GenerateTokenOptions(SigningCredentials signingCredentials, IDictionary<string, object> claims)
        {

            var tokenLifetime = Convert.ToDouble(_configuration.GetValue<string>("Jwt:Lifetime"));

            var tokenExpirationTime = DateTime.UtcNow.AddMinutes(tokenLifetime);

            var token = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetValue<string>("Jwt:Issuer"),
                Audience = _configuration.GetValue<string>("Jwt:Issuer"),
                Claims = claims,
                Expires = tokenExpirationTime,
                SigningCredentials = signingCredentials
            };

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<Customer?> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return user;
        }

        private CookieOptions GetAccessToken()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
            };
        }

        private CookieOptions GetRefreshToken()
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
            };
        }

        public void InvalidateTokenInCookies(HttpContext context)
        {
            var accessToken = GetAccessToken();
            accessToken.Expires = DateTime.UtcNow.AddMinutes(-5);
            context.Response.Cookies.Append("accessToken", "", accessToken);

            var refreshToken = GetRefreshToken();
            refreshToken.Expires = DateTime.UtcNow.AddMinutes(-5);
            context.Response.Cookies.Append("RefreshToken","", refreshToken);
        }

        public void SetTokenInCookies(Tokens tokens, HttpContext context)
        {
            var accessTokenLifetime = Convert.ToDouble(_configuration.GetValue<int>("Jwt:Lifetime"));

            var accesstokenExpirationTime = DateTime.UtcNow.AddMinutes(accessTokenLifetime);

            var accessToken = GetAccessToken();
            accessToken.Expires = accesstokenExpirationTime;

            context.Response.Cookies.Append("accessToken", tokens.AccessToken!,accessToken);


            var refreshTokenLifetime = Convert.ToDouble(_configuration.GetValue<int>("RefreshToken:Lifetime"
                ));

            var refreshtokenExpirationTime = DateTime.UtcNow.AddMinutes(refreshTokenLifetime);

            var refreshToken = GetRefreshToken();
            refreshToken.Expires = refreshtokenExpirationTime;

            context.Response.Cookies.Append("RefreshToken", tokens.RefreshToken!,refreshToken);
        }

        public async Task<TokenValidationResult> ValidateToken(string accessToken)
        {

            var key = _configuration.GetValue<string>("Jwt:key");

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidAudience = _configuration.GetValue<string>("Jwt:Issuer"),
                ValidateLifetime = true,
                ValidIssuer = _configuration.GetValue<string>("Jwt:Issuer"),
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JsonWebTokenHandler();
            return await tokenHandler.ValidateTokenAsync(accessToken, validationParameters);
        }

        public async Task<IdentityResult> DeleteAccount(Customer user)
        {
            return await _userManager.DeleteAsync(user);
        }
    }
}
