using DaiPhuocBE.Contants.Cache;
using DaiPhuocBE.DTOs;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories;
using DaiPhuocBE.Services.CacheServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DaiPhuocBE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class PhuongXaController (IUnitOfWork unitOfWork, ILogger<TinhThanhController> logger, ICacheService cacheService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<TinhThanhController> _logger = logger;
        private readonly ICacheService _cacheService = cacheService;

        [HttpGet]
        public async Task<IActionResult> GetPhuongXa()
        {
            try
            {
                List<Phuongxa> result = new List<Phuongxa>();

                if (await _cacheService.ExistsAsync(CacheKeys.Phuongxa_All))
                {
                    result = await _cacheService.GetAsync<List<Phuongxa>>(CacheKeys.Phuongxa_All);
                }
                else
                {
                    var response = await _unitOfWork.PhuongXaRepository.GetAllAsync();
                    result = response.ToList();

                    await _cacheService.SetAsync(CacheKeys.Phuongxa_All, result,CacheExpirations.DanhMucExpiried);
                }

                    //var result = await _unitOfWork.PhuongXaRepository.GetAllAsync();
                return Ok(new APIResponse<List<Phuongxa>>(success: true, message: "Lấy danh sách phường xã thành công", data: result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong quá trình lấy danh sách phường xã");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }
    }
}
