using DaiPhuocBE.DTOs.AuthDTOs;
using DaiPhuocBE.Services.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaiPhuocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Register register)
        {
            try
            {
                var result = await _authService.Register(register);
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
