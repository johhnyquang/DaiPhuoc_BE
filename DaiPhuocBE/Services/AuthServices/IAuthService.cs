using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.AuthDTOs;

namespace DaiPhuocBE.Services.AuthServices
{
    public interface IAuthService
    {
        Task<APIResponse<LoginResponse>> Register(Register register);
        Task<APIResponse<LoginResponse>> Login(LoginRequest loginRequest);
    }
}
