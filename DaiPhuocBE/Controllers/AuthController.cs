using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.AuthDTOs;
using DaiPhuocBE.Services.AuthServices;
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

        [HttpPost("Login")]
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

        [HttpPost("ChangePassword")]
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
    }
}
