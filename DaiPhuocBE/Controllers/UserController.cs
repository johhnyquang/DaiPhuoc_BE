using DaiPhuocBE.Services.CustomerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaiPhuocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController (IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("GetUserInfo/{id}")]
        public async Task<IActionResult> GetUserInfo(string id)
        {
            try
            {
                var result = await _userService.GetInformationUser(id);
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
