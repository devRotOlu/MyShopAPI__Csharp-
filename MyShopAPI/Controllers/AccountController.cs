using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShopAPI.Core.AuthManager;
using MyShopAPI.Core.EntityDTO;
using MyShopAPI.Core.EntityDTO.UserDTO;
using MyShopAPI.Core.IRepository;
using MyShopAPI.Core.Models;
using MyShopAPI.Data.Entities;
using MyShopAPI.Services.Image;
using System.Security.Claims;

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
        private readonly IPhotoService _photoService;

        public AccountController(IMapper mapper, IAuthManager authManager, IConfiguration configuration, IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            _mapper = mapper;
            _authManager = authManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _photoService = photoService;
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

            user = await _unitOfWork.Customers.Get(customer=>customer.Email == signUpDTO.Email);

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

            var customer = await _unitOfWork.Customers.Get(customer => customer.Email == userDTO.Email,include:customer=>customer.Include(customer=>customer.Details));

            var userInfo = _mapper.Map<CustomerDTO>(customer.Details);
            userInfo.Id = customer.Id;
            userInfo.Email = customer.Email!;

            var refreshToken = _authManager.GenerateRefreshToken();

            var refreshTokenObj = new RefreshToken
            {
                CustomerId = customer.Id,
                Token = refreshToken
            };

            var tokenObj = await _unitOfWork.RefreshTokens.Get(item => item.CustomerId == customer.Id);

            if (tokenObj == null)
            {
                await _unitOfWork.RefreshTokens.Insert(refreshTokenObj);
            }
            else
            {
                refreshTokenObj.Id = tokenObj.Id;
                _unitOfWork.RefreshTokens.Update(refreshTokenObj);
            }

            await _unitOfWork.Save();

            return Accepted(new { accessToken = await _authManager.CreateToken(), user = userInfo, refreshToken });
        }

        [HttpPost("password-reset-email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                return BadRequest();
            }

            await _authManager.GenerateForgotPasswordTokenAsync(user, user.Details.FirstName, _configuration["PasswordConfirmation"]);

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
        public async Task<IActionResult> RefreshToken([FromBody] TokenDTO tokenDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _unitOfWork.RefreshTokens.Get(item => item.CustomerId == tokenDTO.CustomerId && item.Token == tokenDTO.RefreshToken);

            if (result == null || result.ExpirationTime.CompareTo(DateTime.Now) < 0)
            {
                return BadRequest();
            }

            var refreshToken = _authManager.GenerateRefreshToken();

            var refreshTokenObj = _mapper.Map<RefreshToken>(tokenDTO);
            refreshTokenObj.Id = result.Id;
            refreshTokenObj.Token = refreshToken;

            _unitOfWork.RefreshTokens.Update(refreshTokenObj);
            await _unitOfWork.Save();

            return Ok(new { accessToken = await _authManager.CreateToken(), refreshToken });
        }

        [Authorize]
        [HttpPatch("modify-details")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ModifyDetails([FromForm] DetailsDTO detailsDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = _mapper.Map<CustomerDetails>(detailsDTO);

            if (detailsDTO.ProfilePicture != null)
            {
                var photo = await _photoService.AddPhotoAsync(detailsDTO.ProfilePicture);

                if (photo.Error == null)
                {
                    customer.ProfilePictureUrI = photo.SecureUrl.AbsoluteUri;
                    customer.ProfilePicturePublicId = photo.PublicId;
                }
            }

            var userName = User.FindFirstValue(ClaimTypes.Name);

            var result = await _unitOfWork.Customers.Get(customer => customer.Email == userName, include: customer => customer.Include(customer => customer.Details));

            customer.Id = result.Details.Id;
            customer.CustomerId = result.Details.CustomerId;

            _unitOfWork.CustomerDetails.Update(customer);
            await _unitOfWork.Save();


            string uri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}{Request.QueryString}";

            var customerDTO = _mapper.Map<CustomerDTO>(customer);
            customerDTO.Email = result.Email!;

            return Created(uri,new {user=customerDTO});
        }
    }
}
