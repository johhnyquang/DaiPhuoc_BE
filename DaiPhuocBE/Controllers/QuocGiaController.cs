using DaiPhuocBE.Contants.Cache;
using DaiPhuocBE.DTOs;
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
    public class QuocGiaController(IUnitOfWork unitOfWork, ILogger<QuocGiaController> logger, ICacheService cacheService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<QuocGiaController> _logger = logger;
        private readonly ICacheService _cacheService = cacheService; 

        [HttpGet]
        public async Task<IActionResult> GetQuocGia()
        {
            try
            {
                List<QuocGiaResponse> result = new List<QuocGiaResponse>();

                if (await _cacheService.ExistsAsync(CacheKeys.Quocgia_All))
                {
                    result = await _cacheService.GetAsync<List<QuocGiaResponse>>(CacheKeys.Quocgia_All);
                }
                else
                {
                    var response = await _unitOfWork.DmQuocGiaRepository.GetAllAsync();
                    result = response.Select(x => new QuocGiaResponse
                    {
                        MaQuocGia = x.Ma,
                        TenQuocGia = x.Ten ?? "Không xác định",
                    }).ToList();

                    await _cacheService.SetAsync(CacheKeys.Quocgia_All, result, CacheExpirations.DanhMucExpiried);
                }
                    //var result = await _unitOfWork.DmQuocGiaRepository.GetAllAsync();
                return Ok(new APIResponse<List<QuocGiaResponse>>(success: true, message: "Lấy danh sách quốc gia thành công", data: result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong quá trình lấy danh sách quốc gia");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }
    }
}
