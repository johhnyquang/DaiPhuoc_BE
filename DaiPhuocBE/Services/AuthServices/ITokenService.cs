using System.Security.Claims;

namespace DaiPhuocBE.Services.AuthServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token); // Lấy thông tin từ claims từ accessToken hết hạn phục vụ cho refreshtoken
    }
}
