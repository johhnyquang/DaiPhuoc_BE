using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.AuthDTOs;

namespace DaiPhuocBE.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private 
        public AuthService()
        {
            
        }

        public Task<APIResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<APIResponse<LoginResponse>> Register(Register register)
        {
            
        }
    }
}
