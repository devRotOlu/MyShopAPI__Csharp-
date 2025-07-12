using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.DTOs.UserDTOs;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Core.Models;
using MyShopAPI.Data.Entities;

namespace MyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IMapper mapper, IAuthManager authManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _authManager = authManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUP([FromBody] SignUpDTO signUpDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<Customer>(signUpDTO);

            user.UserName = signUpDTO.Email;

            var result = await _authManager.CreateAsync(user, signUpDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            try
            {
                await _authManager.GenerateEmailConfirmationTokenAsync(user, signUpDTO.FirstName, _configuration["EmailConfirmation"]);
            }
            catch
            {

            }
            finally
            {
                await _authManager.AddToRolesAsync(user, new List<string> { "customer" });
            }

            user = await _unitOfWork.Customers.Get(customer => customer.Email == signUpDTO.Email);

            var customerDetails = _mapper.Map<CustomerDetails>(signUpDTO);
            customerDetails.CustomerId = user.Id;

            await _unitOfWork.CustomerDetails.Insert(customerDetails);

            await _unitOfWork.Save();

            return Ok("Registration Successful!. Check your email for validation.");
        }

        [HttpPost("confirm-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ConfirmUserEmail([FromQuery] string uid, [FromQuery] string token)
        {
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            var result = await _authManager.ConfirmEmailAsync(uid, token);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("resend-confirmation-email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResendConfirmationEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var user = await _unitOfWork.Customers.Get(customer => customer.Email == email, include: customer => customer.Include(customer => customer.Details));

            if (user == null)
            {
                return BadRequest();
            }

            await _authManager.GenerateEmailConfirmationTokenAsync(user, user.Details.FirstName, _configuration["AcctValidationEmail"]);
            return Ok("Check your email for validation.");
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authManager.SignInUser(userDTO);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var customer = await _unitOfWork.Customers.Get(customer => customer.Email == userDTO.Email, include: customer => customer.Include(customer => customer.Details));

            var userInfo = _mapper.Map<CustomerDTO>(customer.Details);
            userInfo.Id = customer.Id;
            userInfo.Email = customer.Email!;

            var refreshToken = _authManager.GenerateRefreshToken();

            _authManager.SetTokenInCookies(new Tokens
            {
                AccessToken = await _authManager.CreateToken(userDTO.Email),
                RefreshToken = refreshToken,
            }, HttpContext);

            var refreshTokenObj = await _unitOfWork.RefreshTokens.Get(token => token.CustomerId == customer.Id);

            var expirationTime = _configuration.GetSection("RefreshToken:LifeTime").Value;
            if (refreshTokenObj != null)
            {
                _unitOfWork.RefreshTokens.Update(new RefreshToken
                {
                    Id = refreshTokenObj.Id,
                    CustomerId = customer.Id,
                    Token = refreshToken,
                    ExpirationTime = DateTime.Now.AddMinutes(Convert.ToDouble(expirationTime))
                });
            }
            else
            {
                await _unitOfWork.RefreshTokens.Insert(new RefreshToken
                {
                    CustomerId = customer.Id,
                    Token = refreshToken,
                    ExpirationTime = DateTime.Now.AddMinutes(Convert.ToDouble(expirationTime))
                });
            }

            await _unitOfWork.Save();

            return Accepted(userInfo);
        }

        [HttpPost("password-reset-email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendPasswordResetEmail([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest();
            }

            var user = await _unitOfWork.Customers.Get(customer => customer.Email == email, include: customer => customer.Include(customer => customer.Details));

            if (user == null)
            {
                return Forbid();
            }

            var clientType = Request.Headers["X-Client-Type"].FirstOrDefault()?.ToLower();

            string? rootUrl = null;

            if (clientType == "web")
            {
                rootUrl = $"{Request.Headers["X-Origin"].ToString()}{_configuration["PasswordResetRoute"]}";
            }

            await _authManager.GenerateForgotPasswordTokenAsync(user, user.Details.FirstName, rootUrl);

            return NoContent();
        }

        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authManager.ResetPasswordAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("token_refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken()
        {
            var oldToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(oldToken))
            {
                return BadRequest();
            }

            var refreshTokenObj = await _unitOfWork.RefreshTokens.Get(token => token.Token == oldToken, include: token => token.Include(token => token.Customer));

            if (refreshTokenObj == null || refreshTokenObj.ExpirationTime.CompareTo(DateTime.Now) < 0)
            {
                return BadRequest();
            }

            var refreshToken = _authManager.GenerateRefreshToken();

            _authManager.SetTokenInCookies(new Tokens
            {
                AccessToken = await _authManager.CreateToken(refreshTokenObj.Customer.Email!),
                RefreshToken = refreshToken
            }, HttpContext);

            var expirationTime = _configuration.GetSection("RefreshToken:LifeTime").Value;

            _unitOfWork.RefreshTokens.Update(new RefreshToken
            {
                Id = refreshTokenObj.Id,
                CustomerId = refreshTokenObj.CustomerId,
                Token = refreshToken,
                ExpirationTime = DateTime.Now.AddMinutes(Convert.ToDouble(expirationTime))
            });

            return Ok();
        }

        [Authorize]
        [HttpGet("validate_token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ValidateAccessToken()
        {
            var user = await _authManager.GetUserByEmailAsync(User.Identity!.Name!);

            if (user == null) return BadRequest();

            var result = await _unitOfWork.CustomerDetails.Get(info => info.CustomerId == user.Id);

            var userInfo = _mapper.Map<CustomerDTO>(result);
            userInfo.Id = user.Id;
            userInfo.Email = user.Email!;

            return Ok(userInfo);
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Logout()
        {
            var accessToken = Request.Cookies["accessToken"];
            var refreshToken = Request.Cookies["refreshToken"];

            if (!string.IsNullOrEmpty(accessToken) || !string.IsNullOrEmpty(refreshToken))
            {
                Response.Cookies.Delete("accessToken");
                Response.Cookies.Delete("refreshToken");
            }
            return Ok();
        }

        [Authorize]
        [HttpPatch("modify-details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModifyDetails([FromBody] CustomerDetailsDTO detailsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mappedCustomerDetails = _mapper.Map<CustomerDetails>(detailsDTO);

            var customer = await _authManager.GetUserByEmailAsync(User.Identity!.Name!);

            if (detailsDTO.CurrentPassword is not null && detailsDTO.NewPassword is not null)
            {
                await _authManager.ChangePasswordAsync(customer!, detailsDTO.CurrentPassword, detailsDTO.NewPassword);
            }

            var customerDetails = await _unitOfWork.CustomerDetails.Get(details => details.CustomerId == customer!.Id);

            mappedCustomerDetails.CustomerId = customer!.Id;
            mappedCustomerDetails.Id = customerDetails.Id;

            _unitOfWork.CustomerDetails.Update(mappedCustomerDetails);
            await _unitOfWork.Save();

            var customerDTO = _mapper.Map<CustomerDTO>(customerDetails);
            customerDTO.Email = customer.Email!;

            return Ok(customerDTO);
        }

        [Authorize]
        [HttpPost("add_delivery_profile")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDeliveryProfile([FromBody] AddDeliveryProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var mappedProfile = _mapper.Map<DeliveryProfile>(profileDTO);

            var email = User.Identity!.Name!;

            var user = await _unitOfWork.Customers.Get((user) => user.Email == email);

            var defaultProfile = await _unitOfWork.DeliveryProfiles.Get(profile => profile.IsDefaultProfile == true && profile.CustomerId == user.Id);

            if (defaultProfile != null)
            {
                defaultProfile.IsDefaultProfile = false;
                _unitOfWork.DeliveryProfiles.Update(defaultProfile);
            }

            mappedProfile.CustomerId = user.Id;
            mappedProfile.IsDefaultProfile = true;

            await _unitOfWork.DeliveryProfiles.Insert(mappedProfile);

            await _unitOfWork.Save();

            return Created("", mappedProfile);
        }

        [Authorize]
        [HttpGet("get_delivery_profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDeliveryProfiles([FromQuery] string userId)
        {

            var user = await _authManager.GetUserByIdAsync(userId);

            if (user is null)
            {
                return BadRequest();
            }

            var profiles = await _unitOfWork.DeliveryProfiles.GetAll(profile => profile.CustomerId == userId && profile.IsDeleted != true).ToListAsync();

            var profileDTOs = _mapper.Map<IEnumerable<DeliveryProfileDTO>>(profiles);

            return Ok(profileDTOs);
        }

        [Authorize]
        [HttpPatch("modify_delivery_profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModifyDeliveryProfile([FromBody] DeliveryProfileDTO profileDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mappedProfile = _mapper.Map<DeliveryProfile>(profileDTO);

            var user = await _unitOfWork.Customers.Get((user) => user.Email == User.Identity!.Name);

            mappedProfile.CustomerId = user.Id;

            _unitOfWork.DeliveryProfiles.Update(mappedProfile);

            await _unitOfWork.Save();

            var profiles = await _unitOfWork.DeliveryProfiles.GetAll(profile => profile.CustomerId == user.Id).ToListAsync();

            var profileDTOs = _mapper.Map<IEnumerable<DeliveryProfileDTO>>(profiles);

            return Ok(profileDTOs);
        }

        [Authorize]
        [HttpDelete("delete_delivery_profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDeliveryProfile([FromQuery] int profileId)
        {
            if (profileId < 0)
                return BadRequest();

            var user = await _authManager.GetUserByEmailAsync(User!.Identity!.Name!);

            var profile = await _unitOfWork.DeliveryProfiles.Get((profile) => profile.Id == profileId && profile.CustomerId == user!.Id);

            if (profile is null)
                return BadRequest();

            profile.IsDeleted = true;
            _unitOfWork.DeliveryProfiles.Update(profile);

            await _unitOfWork.Save();

            return Ok();
        }

        [Authorize]
        [HttpDelete("delete_account")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _authManager.GetUserByEmailAsync(User.Identity!.Name!);

            var result = await _authManager.DeleteAccount(user!);

            if (!result.Succeeded)
                return BadRequest();

            return LocalRedirect("~/api/Account/logout");
        }
    }
}
