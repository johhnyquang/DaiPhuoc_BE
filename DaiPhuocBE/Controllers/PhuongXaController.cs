using DaiPhuocBE.DTOs;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaiPhuocBE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PhuongXaController (IUnitOfWork unitOfWork, ILogger<TinhThanhController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<TinhThanhController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetPhuongXa()
        {
            try
            {
                var result = await _unitOfWork.PhuongXaRepository.GetAllAsync();
                return Ok(new APIResponse<List<Phuongxa>>(success: true, message: "Lấy danh sách phường xã thành công", data: result.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong quá trình lấy danh sách phường xã");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }
    }
}
