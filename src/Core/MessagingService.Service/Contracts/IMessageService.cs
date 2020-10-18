using System.Collections.Generic;
using System.Threading.Tasks;
using MessagingService.Dto;

namespace MessagingService.Service
{
    public interface IMessageService
    {
        Task<ServiceResponse<bool>> SendMessage(SendMessageDto model);
        Task<ServiceResponse<PagedUserMessageDto>> GetAllMessages(ReadUserMessageDto model);
        Task<ServiceResponse<PagedUserMessageDto>> GetUnreadMessages(ReadUserMessageDto model);
    }
}