using System;
using System.Collections.Generic;
using MessagingService.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MessagingService.Api.Controllers
{
    [ApiController]
    [Authorize]
    [ValidateModel]
    [ApiExceptionFilter]
    [Route("[controller]")]
    public class BaseController : Controller
    {
        protected string UserId { get; set; }
        protected string UserName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = User.FindFirst("UserId");
            var userName = User.FindFirst("UserName");

            if (userId != null && userName != null)
            {
                UserId = userId.Value;
                UserName = userName.Value;
            }

            base.OnActionExecuting(context);
        }

        [NonAction]
        protected IActionResult Success<T>(T data, string message) =>
            StatusCode(200, new ApiData<T>
            {
                Success = true,
                Data = data,
                Message = message
            });

        [NonAction]
        protected IActionResult SuccessPaged<T>(List<T> data, int page, int perPage, int totalPages, string message) =>
            StatusCode(200, new PagedApiData<T>
            {
                Success = true,
                Data = data,
                Message = message,
                Page = page,
                PerPage = perPage,
                TotalPages = totalPages
            });

        [NonAction]
        protected IActionResult BadRequest<T>(T data) => StatusCode(400, new ApiData<T> {Success = false, Data = data});

        [NonAction]
        protected IActionResult BadRequest<T>(T data, string message) => StatusCode(400,
            new ApiData<T> {Success = false, Data = data, Message = message});

        [NonAction]
        protected IActionResult BadRequest(string[] errors, string message) => StatusCode(400,
            new ApiData<string[]> {Success = false, Data = errors, Message = message});

        [NonAction]
        protected IActionResult Unauthorized<T>(T data) =>
            StatusCode(401, new ApiData<T> {Success = false, Data = data});

        [NonAction]
        protected IActionResult Forbidden<T>(T data) => StatusCode(403, new ApiData<T> {Success = false, Data = data});

        [NonAction]
        protected IActionResult NotFound<T>(T data) => StatusCode(404, new ApiData<T> {Success = false, Data = data});

        [NonAction]
        protected IActionResult Error(string message, string[] errors) => StatusCode(500,
            new ApiData<string[]> {Success = false, Data = errors, Message = message});
    }
}