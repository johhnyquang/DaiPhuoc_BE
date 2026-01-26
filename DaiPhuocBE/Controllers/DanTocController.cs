using DaiPhuocBE.Contants.Cache;
using DaiPhuocBE.DTOs;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories;
using DaiPhuocBE.Services.CacheServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace DaiPhuocBE.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DanTocController(IUnitOfWork unitOfWork, ILogger<DanTocController> logger, ICacheService cacheService) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ILogger<DanTocController> _logger = logger;
        private readonly ICacheService _cacheService = cacheService;

        [HttpGet]
        public async Task<IActionResult> GetDanToc()
        {
            try
            {
                List<DanTocResponse> result = new List<DanTocResponse>();

                if (await _cacheService.ExistsAsync(CacheKeys.Dantoc_All))
                {
                    // nếu như là true thì có cache
                    result =  await _cacheService.GetAsync<List<DanTocResponse>>(CacheKeys.Dantoc_All);
                }
                else
                {
                    var response = await _unitOfWork.DanTocRepository.GetAllAsync();
                    result = response.Select(x => new DanTocResponse 
                    {
                        Madantoc = x.Madantoc,
                        Dantoc = x.Dantoc,
                        Hide = x.Hide
                    }).ToList();

                    // set vào cache
                    await _cacheService.SetAsync(CacheKeys.Dantoc_All, result, CacheExpirations.DanhMucExpiried);

                }

                return Ok(new APIResponse<List<DanTocResponse>>(success: true, message: "Lấy danh sách dân tộc thành công", data: result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi trong quá trình lấy danh sách dân tộc");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error {ex.Message}");
            }
        }
    }
}
