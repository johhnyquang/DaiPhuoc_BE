using DaiPhuocBE.DTOs;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaiPhuocBE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DanTocController(IUnitOfWork unitOfWork, ILogger<DanTocController> logger) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<DanTocController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetDanToc()
        {
            try
            {
                var result = await _unitOfWork.DanTocRepository.GetAllAsync();
                return Ok(new APIResponse<List<Btddt>>(success: true, message: "Lấy danh sách dân tộc thành công", data: result.ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong quá trình lấy danh sách dân tộc");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }
    }
}
