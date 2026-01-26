using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.UserDTOs;

namespace DaiPhuocBE.Services.CustomerServices
{
    public interface IUserService
    {
        Task<APIResponse<UserResponse>> GetInformationUser(string cccd);
    }
}
