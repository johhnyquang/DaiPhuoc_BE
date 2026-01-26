using DaiPhuocBE.Services.CustomerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaiPhuocBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController (ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        [HttpGet("GetCustomerInfo/{id}")]
        public async Task<IActionResult> GetCustomerInfo (string id)
        {
            try
            {
                var result = await _customerService.GetInfoCustomer(id);
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
