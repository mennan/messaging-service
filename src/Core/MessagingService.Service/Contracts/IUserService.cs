using System.Collections.Generic;
using System.Threading.Tasks;
using MessagingService.Dto;

namespace MessagingService.Service
{
    public interface IUserService
    {
        Task<ServiceResponse<bool>> Register(RegisterDto model);
        Task<ServiceResponse<string>> Login(LoginDto model);
        Task<ServiceResponse<bool>> BlockUser(BlockUserDto model);
    }
}