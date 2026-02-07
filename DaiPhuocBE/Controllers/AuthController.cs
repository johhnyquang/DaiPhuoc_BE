using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.AuthDTOs;
using DaiPhuocBE.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Swashbuckle.AspNetCore.Annotations;

namespace DaiPhuocBE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        /// <summary>
        /// Đăng ký user mới trong hệ thống
        /// </summary>
        /// <param name="register">Thông tin người dùng cần đăng ký</param>
        /// <returns>Tạo user thành công và gọi hàm Login để thực hiện đăng nhập</returns>
        [HttpPost("Register")]
        [SwaggerOperation(Summary = "Tạo user mới")]
        [SwaggerResponse(201, "Created", typeof(LoginResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(APIResponse<object>))]
        public async Task<IActionResult> RegisterAsync([FromBody] Register register)
        {
            try
            {
                var request = register;
                request.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                Console.WriteLine(request.IpAddress);

                var result = await _authService.Register(request);
                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return CreatedAtAction(nameof(Login),result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }

        /// <summary>
        /// User đăng nhập vào hệ thống
        /// </summary>
        /// <param name="loginRequest">Thông tin đăng nhập</param>
        /// <returns>User đăng nhập thành công</returns>
        [HttpPost("Login")]
        [SwaggerOperation(Summary = "User đăng nhập")]
        [SwaggerResponse(200, "OK", typeof(LoginResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(APIResponse<object>))]

        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var request = loginRequest;
                request.IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                var result = await _authService.Login(loginRequest);
                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }

        /// <summary>
        /// Đổi mật khẩu user
        /// </summary>
        /// <param name="changePassword">Mật khẩu mới</param>
        /// <returns>Đổi mật khẩu thành công</returns>
        [HttpPost("ChangePassword")]
        [SwaggerOperation(Summary = "User đổi mật khẩu")]
        [SwaggerResponse(200, "OK", typeof(LoginResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(APIResponse<object>))]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] LoginRequest changePassword)
        {
            try
            {
                var result = await _authService.ChangePassword(changePassword);
                if (!result.Success)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }

        /// <summary>
        /// Cấp lại token cho user
        /// </summary>
        /// <param name="rotateRequest">Cấp lại token</param>
        /// <returns>User đăng nhập thành công</returns>
        [SwaggerOperation(Summary = "Mobile app gửi ngầm refreshtoken")]
        [SwaggerResponse(200, "OK", typeof(LoginResponse))]
        [SwaggerResponse(400, "Bad Request", typeof(APIResponse<object>))]
        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RotateModel rotateRequest)
        {
            try
            {
                var result = await _authService.RotationToken(rotateRequest);
                if (!result.Success)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await _authService.Logout();
                return Ok(new
                {
                    Success = true,
                    Message = "Đăng xuất thành công",
                    Apiversion = "V1"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }   
        }
    }
}
