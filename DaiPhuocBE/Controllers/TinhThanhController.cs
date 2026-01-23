using DaiPhuocBE.Data;
using DaiPhuocBE.DTOs;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DaiPhuocBE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TinhThanhController (IUnitOfWork unitOfWork, ILogger<TinhThanhController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<TinhThanhController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetTinhThanh()
        {
            try
            {
                var result = await _unitOfWork.TinhThanhRepository.GetAllAsync();
                return Ok(new APIResponse<List<Tinhthanh>>(success: true, message: "Lấy danh sách tỉnh thành thành công", data: result.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong quá trình lấy danh sách tỉnh thành");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }
    }
}
