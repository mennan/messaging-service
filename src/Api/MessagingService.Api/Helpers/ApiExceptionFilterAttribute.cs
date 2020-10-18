using System;
using System.Net;
using System.Threading.Tasks;
using DnsClient.Internal;
using MessagingService.Api.Models;
using MessagingService.Dto;
using MessagingService.Repository;
using MessagingService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingService.Api
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            var errorRepository = context.HttpContext.RequestServices.GetService<IErrorService>();
            var exception = context.Exception;
            
            await errorRepository.Save(new ErrorDto
            {
                Message = exception.Message,
                StackTrace = exception.StackTrace
            });
            
            context.Exception = null;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new ApiData<object>()
            {
                Message = "Internal server error. Please contact your system administrator.",
            });
            
            await base.OnExceptionAsync(context);
        }
    }
}