using System.Threading.Tasks;
using MessagingService.Dto;

namespace MessagingService.Service
{
    public interface IErrorService
    {
        Task<ServiceResponse<bool>> Save(ErrorDto model);
    }
}