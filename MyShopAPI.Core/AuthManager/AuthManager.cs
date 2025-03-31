using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MyShopAPI.Core.EmailMananger;
using MyShopAPI.Core.EntityDTO.UserDTO;
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
        private static Customer _user;
        private readonly SignInManager<Customer> _signInManager;
        private readonly IEmailManager _emailManager;

        public Customer User => _user;

        public AuthManager(UserManager<Customer> userManager, IConfiguration configuration, IEmailManager emailManager, SignInManager<Customer> signInManager)
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

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            var user = await _userManager.FindByIdAsync(uid);
            var decodedToken = Base64UrlEncoder.Decode(token);
            return await _userManager.ConfirmEmailAsync(user, decodedToken);
        }

        public async Task<IdentityResult> CreateAsync(Customer appUser, string password)
        {
            return await _userManager.CreateAsync(appUser, password);
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();

            var claims = await GetClaims();

            var tokenDescriptor = GenerateTokenOptions(signingCredentials, claims);

            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }

        public async Task<IdentityResult> ChangePasswordAsync(Customer user,string currentPassword,string newPassword)
        {
            try
            {
                return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            }
            catch (Exception)
            {

                throw;
            }
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
            AuthManager._user = await _userManager.FindByEmailAsync(userDTO.Email);

            return await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, false);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = _configuration["Jwt:key"];

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<IDictionary<string, object>> GetClaims()
        {
            var claims = new Dictionary<string, object>();
            claims.Add(ClaimTypes.Name, User.UserName!);
            claims.Add(JwtRegNamesCalims.Aud, _configuration["Jwt:Issuer"]!);

            var roles = await _userManager.GetRolesAsync(User);

            foreach (var role in roles)
            {
                claims.Add(ClaimTypes.Role, role);
            }

            return claims;
        }

        private SecurityTokenDescriptor GenerateTokenOptions(SigningCredentials signingCredentials, IDictionary<string, object> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var tokenLifetime = Convert.ToDouble(jwtSettings.GetSection("Lifetime").Value);

            var tokenExpirationTime = DateTime.Now.AddMinutes(tokenLifetime);

            var token = new SecurityTokenDescriptor
            {
                Issuer = jwtSettings.GetSection("Issuer").Value,
                Audience = jwtSettings.GetSection("Issuer").Value,
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

        public void SetTokenInCookies(Tokens tokens, HttpContext context)
        {
            var accessTokenLifetime = Convert.ToDouble(_configuration.GetSection("Jwt:Lifetime").Value);

            var accesstokenExpirationTime = DateTime.Now.AddMinutes(accessTokenLifetime);

            context.Response.Cookies.Append("accessToken", tokens.AccessToken!, new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Expires = accesstokenExpirationTime
            });

            var refreshTokenLifetime = Convert.ToDouble(_configuration.GetSection("refreshToken:Lifetime").Value);

            var refreshtokenExpirationTime = DateTime.Now.AddMinutes(refreshTokenLifetime);
            context.Response.Cookies.Append("refreshToken", tokens.RefreshToken!, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = refreshtokenExpirationTime
            });
        }

        public async Task<TokenValidationResult> ValidateToken(string accessToken)
        {

            var key = _configuration.GetSection("Jwt:key").Value;

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidAudience = _configuration.GetSection("Jwt:Issuer").Value,
                ValidateLifetime = true,
                ValidIssuer = _configuration.GetSection("Jwt:Issuer").Value,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JsonWebTokenHandler();
            var securityToken = tokenHandler.ReadToken(accessToken);
            return await tokenHandler.ValidateTokenAsync(securityToken, validationParameters);
        }

        public async Task<IdentityResult> DeleteAccount(Customer user)
        {
            return await _userManager.DeleteAsync(user);
        }
    }
}
