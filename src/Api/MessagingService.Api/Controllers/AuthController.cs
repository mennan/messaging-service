using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MessagingService.Api.Models;
using MessagingService.Dto;
using MessagingService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MessagingService.Api.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IJwtHelper _jwtHelper;

        public AuthController(IUserService userService, IJwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model">User info</param>
        /// <response code="200">Returns success message</response>
        /// <response code="400">If user registered before or any validation errors</response>   
        /// <response code="500">If connection problem</response>  
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiData<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var result = await _userService.Register(new RegisterDto
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password
            });

            return result.Success ? Success(true, result.Message) : BadRequest(result.Errors, result.Message);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="model">Username and password info</param>
        /// <response code="200">Returns JWT access token</response>
        /// <response code="400">If invalid username/password combination or any validation errors</response>   
        /// <response code="500">If connection problem</response>  
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiData<LoginResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.Login(new LoginDto
            {
                Name = model.Name,
                Password = model.Password
            });

            if (!result.Success) return BadRequest(result.Errors, result.Message);

            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _jwtHelper.GetJwtSecret();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", result.Data),
                    new Claim("UserName", model.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var generatedToken = tokenHandler.WriteToken(token);

            return Success(new LoginResponseModel {AccessToken = generatedToken}, "Login successfully.");
        }
    }
}