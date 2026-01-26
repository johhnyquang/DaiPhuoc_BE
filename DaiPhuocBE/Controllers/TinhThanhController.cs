using DaiPhuocBE.Contants.Cache;
using DaiPhuocBE.Data;
using DaiPhuocBE.DTOs;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories;
using DaiPhuocBE.Services.CacheServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DaiPhuocBE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TinhThanhController (IUnitOfWork unitOfWork, ILogger<TinhThanhController> logger, ICacheService cacheService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<TinhThanhController> _logger = logger;
        private readonly ICacheService _cacheService = cacheService;

        [HttpGet]
        public async Task<IActionResult> GetTinhThanh()
        {
            try
            {
                List<Tinhthanh> result = new List<Tinhthanh>();

                if (await _cacheService.ExistsAsync(CacheKeys.Tinhthanh_All))
                {
                    result = await _cacheService.GetAsync<List<Tinhthanh>>(CacheKeys.Tinhthanh_All);
                }
                else
                {
                    var response = await _unitOfWork.TinhThanhRepository.GetAllAsync();
                    result = response.ToList();

                    await _cacheService.SetAsync(CacheKeys.Tinhthanh_All, result, CacheExpirations.DanhMucExpiried);
                }
                return Ok(new APIResponse<List<Tinhthanh>>(success: true, message: "Lấy danh sách tỉnh thành thành công", data: result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong quá trình lấy danh sách tỉnh thành");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }
    }
}
