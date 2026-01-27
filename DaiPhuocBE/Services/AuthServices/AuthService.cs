using DaiPhuocBE.DependencyInjection.Options;
using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.AuthDTOs;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories;
using DaiPhuocBE.Repositories.UserRepository;
using DaiPhuocBE.Services.CacheServices;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DaiPhuocBE.Services.AuthServices
{
    public class AuthService(
        ITokenService tokenService, 
        IUnitOfWork unitOfWork, 
        ILogger<AuthService> logger, 
        IOptions<JwtSettings> options,
        ICacheService cacheService,
        IHttpContextAccessor http) : IAuthService
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<AuthService> _logger = logger;
        private readonly JwtSettings _jwtSettings = options.Value;
        private readonly ICacheService _cacheService = cacheService;
        private readonly IHttpContextAccessor _http = http;

        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private async Task<LoginResponse> GenerateTokens (IEnumerable<Claim> claims, User user, LoginRequest? loginRequest, RotateModel? rotateRequest)
        {
            // Bắt đầu GenerateAccessToken và RefreshToken
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // Insert cache chỉ lưu refreshtoken
            // Nếu cache tồn tại thì xóa và insert lại 
            if (await _cacheService.ExistsAsync($"Refreshs:{user.Id}"))
            {
                await _cacheService.RemoveAsync($"Refreshs:{user.Id}");
            }

            // Nếu cache chưa tồn tại thì insert
            var value = new RefreshTokenModel
            {
                RefreshToken = refreshToken,
                DeviceId = loginRequest != null ? loginRequest.DeviceId : "",
                IpAddress = loginRequest != null ? loginRequest.IpAddress : "",
                CreatedAt = DateTime.UtcNow,
            };

            var loginResponse = new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays),
                Id = user.Id,
                Role = "User"
            };
            await _cacheService.SetAsync($"Refreshs:{user.Id}", value, loginResponse.RefreshTokenExpiryTime.TimeOfDay);

            return loginResponse;
        }

        public async Task<APIResponse<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var inputValidation = InputValidation(loginRequest, null);

            if (!inputValidation.Item1)
            {
                return new APIResponse<LoginResponse>(success: inputValidation.Item1, message: inputValidation.Item2);
            }

            try
            {
                var user = await _unitOfWork.UserRepository.GetUserBySocmndAsync(loginRequest.CCCD);
                if (user == null)
                {
                    return new APIResponse<LoginResponse>(success: false, message: "CCCD hoặc mật khẩu không chính xác", null);
                }

                // Nếu như user tồn tại thì check password 
                bool verifyPassword = VerifyPassword(loginRequest.Password, user.PasswordHash);

                if (!verifyPassword)
                {
                    return new APIResponse<LoginResponse>(
                        success: false,
                        message: "CCCD hoặc mật khẩu không chính xác"
                    );
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user .Id.ToString()),
                    new Claim(ClaimTypes.Name, user .Hoten),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID cho token
                    new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()), // Issued at
                };

                // Bắt đầu GenerateAccessToken và RefreshToken
                var loginResponse = await GenerateTokens(claims, user, loginRequest, null);
                return new APIResponse<LoginResponse>(success: true, message: "Đăng nhập thành công", loginResponse);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đăng nhập user");

                return new APIResponse<LoginResponse>(
                    success: false,
                    message: $"Đã xảy ra lỗi trong quá trình đăng nhập",
                    data: null
                );
            }
        }

        public async Task<APIResponse<LoginResponse>> Register(Register register)
        {
            var inputValidation = InputValidation(null, register);

            if (!inputValidation.Item1)
            {
                return new APIResponse<LoginResponse>(success: inputValidation.Item1, message: inputValidation.Item2);
            }

            if (!IsValidPhoneNumber(register.SDT))
            {
                return new APIResponse<LoginResponse>(
                    success: false,
                    message: "Số điện thoại không hợp lệ (phải là 10 số, bắt đầu bằng 0)",
                    null
                );
            }

            try
            {
                bool checkExistsCCCD = await _unitOfWork.UserRepository.IsSocmndExistsAsync(register.CCCD);
                if (checkExistsCCCD == true)
                {
                    return new APIResponse<LoginResponse>(success: false, message: "CCCD này đã tồn tại", null);
                }

                // Hashpassword
                (bool, string) passwordHash = HashPassword(register.Password);
                if (!passwordHash.Item1)
                {
                    return new APIResponse<LoginResponse>(success: passwordHash.Item1, message: passwordHash.Item2);
                }

                int idUserMax = await _unitOfWork.UserRepository.MaxAsync(u => u.Id);

                // Đăng ký thông tin hành chính
                var newUser = new User
                {
                    Id = idUserMax +1 ,
                    Socmnd = register.CCCD,
                    PasswordHash = passwordHash.Item2,
                    Sdt = register.SDT,
                    Hoten = register.HOTEN,
                    Ngaysinh = register.NgaySinh,
                    Namsinh = register.NgaySinh.HasValue ? register.NgaySinh.Value.ToString("yyyy") : "1900",
                    Phai = register.Phai.HasValue == true ? true : false,
                    Quoctich = register.QuocTich,
                    Dantoc = register.DanToc,
                    Matinh = register.TinhThanh,
                    Maphuongxa = register.TinhThanh+register.PhuongXa,
                    Email = register.Email,
                };

                await _unitOfWork.UserRepository.AddAsync(newUser);
                await _unitOfWork.SaveChangesAsync();

                var loginRequest = new LoginRequest
                {
                    CCCD = register.CCCD,
                    Password = register.Password,
                    DeviceId = register.DeviceId ?? string.Empty,
                    IpAddress = register.IpAddress ?? string.Empty
                };

                return await Login(loginRequest);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex, "Lỗi khi đăng ký user");

                return new APIResponse<LoginResponse>(
                    success: false,
                    message: $"Đã xảy ra lỗi trong quá trình đăng ký",
                    data: null
                );
            }
        }
        public async Task<APIResponse<LoginResponse>> ChangePassword(LoginRequest changePassword)
        {
            var inputValidation = InputValidation(changePassword, null);

            if (!inputValidation.Item1)
            {
                return new APIResponse<LoginResponse>(success: inputValidation.Item1, message: inputValidation.Item2);
            }

            try
            {
                var user = await _unitOfWork.UserRepository.GetUserBySocmndAsync(changePassword.CCCD);
                if (user == null)
                {
                    return new APIResponse<LoginResponse>(success: false, message: "CCCD không tồn tại mời bạn đăng ký", null);
                }

                (bool, string) passwordUpdateHash = HashPassword(changePassword.Password);
                if (!passwordUpdateHash.Item1)
                {
                    return new APIResponse<LoginResponse>(success: passwordUpdateHash.Item1, message: passwordUpdateHash.Item2);
                }

                // Lấy thông tin user ra để update lại password
                user.PasswordHash = passwordUpdateHash.Item2;
                await _unitOfWork.SaveChangesAsync();

                return new APIResponse<LoginResponse>(success: true, message: "Đổi mật khẩu thành công vui lòng đăng nhập lại", null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi đổi mật khẩu");

                return new APIResponse<LoginResponse>(
                    success: false,
                    message: $"Đã xảy ra lỗi trong quá trình đổi mật khẩu",
                    data: null
                );
            }
        }

        public async Task Logout()
        {

            var userId = _http.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var jti = _http.HttpContext?.User.FindFirst("jti")?.Value;

            // Set blacklist accesstoken
            await _cacheService.SetAsync($"Blacklist:{jti}", "revoked", TimeSpan.FromMinutes(15)); // những accesstoken nào có jti nào trong list này thì sẽ được đánh dấu là đã loại bỏ "revoked"

            // Delete refreshToken
            await _cacheService.RemoveAsync($"Refreshs:{userId}"); // Vì là logout nên refreshtoken còn hạn cũng cho xóa luôn
        }

        private (bool, string) PasswordValidation(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return (false, "Mật khẩu không được để trống");
            }

            if (password.Length < 8)
            {
                return (false, "Mật khẩu ít nhất 8 ký tự");
            }

            if (password.Contains(" "))
            {
                return (false, "Mật khẩu không được chứa khoảng trắng");
            }

            bool isFirstUpper = char.IsUpper(password[0]);

            if (!isFirstUpper)
            {
                return (false, "Ký tự đầu tiên phải in hoa");
            }

            if (!Regex.IsMatch(password, @"[^a-zA-Z0-9]"))
            {
                return (false, "Chuỗi phải có ký tự đặt biệt");
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                return (false, "Chuỗi ít nhất phải có ký tự số");
            }

            return (true, "Mật khẩu hợp lệ");
        }

        private (bool,string) HashPassword(string password)
        {
            var (isValid, message) = PasswordValidation(password);
            if (!isValid)
            {
                return (false, message);
            }

            // BCrypt tự động generate salt và lưu trong hash
            string hash = BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
            return (true, hash);
        }

        private bool VerifyPassword(string password, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhoneNumber(string sdt)
        {
            // SĐT Việt Nam: 10 số, bắt đầu bằng 0
            return !string.IsNullOrWhiteSpace(sdt) &&
                   Regex.IsMatch(sdt, @"^0\d{9}$");
        }

        private (bool, string) InputValidation (LoginRequest? loginRequest, Register? register)
        {
            return (loginRequest, register) switch
            {
                ({ CCCD: null or "" }, _) => (false, "CCCD không được để trống"),
                ({ Password: null or "" }, _) => (false, "Password không được để trống"),
                (null, { CCCD: null or "" }) => (false, "CCCD không được để trống"),
                (null, { SDT: null or "" }) => (false, "SDT không được để trống"),
                (null, { HOTEN: null or "" }) => (false, "Họ và tên không được để trống"),
                (null, { Password: null or "" }) => (false, "Mật khẩu không được để trống"),
                (null, { NgaySinh: null}) => (false, "Ngày sinh không được để trống"),
                (null, { Phai: null }) => (false, "Giới tính không được để trống"),
                (null, { QuocTich: null or "" }) => (false, "Quốc tịch không được để trống"),
                (null, { DanToc: null or "" }) => (false, "Dân tộc không được để trống"),
                (null, { TinhThanh: null or "" }) => (false, "Tỉnh thành không được để trống"),
                (null, { PhuongXa: null or "" }) => (false, "Phường xã không được để trống"),
                _ => (true, "Thỏa mãn điều kiện")
            };
        }

        public async Task<APIResponse<LoginResponse>> RotationToken(RotateModel rotateRequest)
        {
            // Lấy thông tin user từ accessToken đã hết hạn
            var principal = _tokenService.GetPrincipalFromExpiredToken(rotateRequest.AccessToken);
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return new APIResponse<LoginResponse>(success: false, message: "Invalid accessToken", null);
            }

            // Tìm RefreshToken trong cache
            var cachedRefresh = await _cacheService.GetAsync<RefreshTokenModel>($"Refreshs:{userId}");
            if (cachedRefresh == null || cachedRefresh.RefreshToken != rotateRequest.RefreshToken)
            {
                return new APIResponse<LoginResponse>(success: false, message: "Invalid refreshToken", null);
            }

            // Xóa refreshToken cũ trong cache
            await _cacheService.RemoveAsync($"Refreshs:{userId}");
 
            // Tạo mới accessToken và refreshToken
            var user = await _unitOfWork.UserRepository.GetByIdAsync(int.Parse(userId));

            if (user == null)
            {
                return new APIResponse<LoginResponse>(success: false, message: "User not found", null);
            }

            LoginResponse newToken = await GenerateTokens(principal.Claims,user, null, rotateRequest);

            return new APIResponse<LoginResponse>(success: true, message: "Rotation successfully", newToken);
        }
    }
}
