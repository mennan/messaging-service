using System.Threading.Tasks;
using MessagingService.Api.Models;
using MessagingService.Dto;
using MessagingService.Repository;
using MessagingService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MessagingService.Api.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// Get all user messages
        /// </summary>
        /// <param name="page">Current page number</param>
        /// <param name="perPage">Item per page count</param>
        /// <response code="200">Returns user messages</response>
        /// <response code="400">If any validation errors</response>   
        /// <response code="500">If connection problem</response>  
        [HttpGet("all")]
        [ProducesResponseType(typeof(PagedApiData<UserMessageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AllMessages([FromQuery(Name = "page")] int page = 1,
            [FromQuery(Name = "per_page")] int perPage = 100)
        {
            var messageResult = await _messageService.GetAllMessages(new ReadUserMessageDto
            {
                Name = UserName,
                Page = page,
                PerPage = perPage
            });

            if (messageResult.Success)
                return SuccessPaged(messageResult.Data.Messages, page, perPage, messageResult.Data.TotalPage,
                    messageResult.Message);

            return BadRequest(messageResult.Errors, messageResult.Message);
        }

        /// <summary>
        /// Get all unread user messages
        /// </summary>
        /// <param name="page">Current page number</param>
        /// <param name="perPage">Item per page count</param>
        /// <response code="200">Returns unread user messages</response>
        /// <response code="400">If any validation errors</response>   
        /// <response code="500">If connection problem</response>  
        [HttpGet("unread")]
        [ProducesResponseType(typeof(PagedApiData<UserMessageDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnreadMessages([FromQuery(Name = "page")] int page = 1,
            [FromQuery(Name = "per_page")] int perPage = 100)
        {
            var messageResult = await _messageService.GetUnreadMessages(new ReadUserMessageDto
            {
                Name = UserName,
                Page = page,
                PerPage = perPage
            });

            if (messageResult.Success)
                return SuccessPaged(messageResult.Data.Messages, page, perPage, messageResult.Data.TotalPage,
                    messageResult.Message);

            return BadRequest(messageResult.Errors, messageResult.Message);
        }

        /// <summary>
        /// Send a message to a user whose name is known
        /// </summary>
        /// <param name="model">Message info</param>
        /// <response code="200">Returns success message</response>
        /// <response code="400">If any validation errors</response>   
        /// <response code="500">If connection problem</response>  
        [HttpPost("send")]
        [ProducesResponseType(typeof(ApiData<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiData<string[]>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageModel model)
        {
            var sendResult = await _messageService.SendMessage(new SendMessageDto
            {
                FromUser = UserName,
                ToUser = model.To,
                Content = model.Content
            });

            if (sendResult.Success) return Success(true, sendResult.Message);

            return BadRequest(sendResult.Errors, sendResult.Message);
        }
    }
}