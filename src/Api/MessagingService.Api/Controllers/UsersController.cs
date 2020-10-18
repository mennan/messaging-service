using System.Threading.Tasks;
using MessagingService.Api.Models;
using MessagingService.Dto;
using MessagingService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessagingService.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// Block user you don't want to receive messages
        /// </summary>
        /// <param name="model">Block user information</param>
        /// <response code="200">Returns success message</response>
        /// <response code="400">If any validation errors</response>   
        /// <response code="500">If connection problem</response>  
        [HttpPost("block")]
        [ProducesResponseType(typeof(ApiData<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserModel model)
        {
            var blockResult = await _userService.BlockUser(new BlockUserDto
            {
                BlockedByUser = UserName,
                BlockedUser = model.UserName
            });

            if (blockResult.Success) return Success(true, blockResult.Message);

            return BadRequest(blockResult.Errors, blockResult.Message);
        }
    }
}