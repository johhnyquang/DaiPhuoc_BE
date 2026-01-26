using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.AuthDTOs;
using DaiPhuocBE.DTOs.UserDTOs;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories;
using DaiPhuocBE.Services.CustomerServices;
using System.Threading.Tasks;

namespace DaiPhuocBE.Services.UserServices;

public class UserService (IUnitOfWork unitOfWork, ILogger<UserService> logger) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<UserService> _logger = logger;
    // có lưu cache

    private async Task<Dmquocgium> GetInfoDmQuocGiaByIdAsync(string maQuocGia) => await _unitOfWork.DmQuocGiaRepository.GetByIdAsync(maQuocGia);
    private async Task<Tinhthanh> GetInfoTinhThanhByIdAsync(string maTT) => await _unitOfWork.TinhThanhRepository.GetByIdAsync(maTT);
    private async Task<Phuongxa> GetInfoPhuongXaByIdAsync(string maPX) => await _unitOfWork.PhuongXaRepository.GetByIdAsync(maPX);
    private async Task<Btddt> GetInfoDanTocByIdAsync(string maDT) => await _unitOfWork.DanTocRepository.GetByIdAsync(maDT);

    public async Task<APIResponse<UserResponse>> GetInformationUser(string cccd)
    {
        if (string.IsNullOrWhiteSpace(cccd))
        {
            return new APIResponse<UserResponse>(success: false, message: "CCCD không được để trống", data: null);
        }

        try
        {
            var userInfo = await _unitOfWork.UserRepository.GetUserBySocmndAsync(cccd);
            if (userInfo == null)
            {
                return new APIResponse<UserResponse>(success: false, message: "Không tìm thấy thông tin vui lòng đăng ký", data: null);
            }

            var quocGia = await GetInfoDmQuocGiaByIdAsync(userInfo.Quoctich);
            var tinhthanh = await GetInfoTinhThanhByIdAsync(userInfo.Matinh);
            var phuongxa = await GetInfoPhuongXaByIdAsync(userInfo.Maphuongxa.Substring(userInfo.Matinh.Length));
            var dantoc = await GetInfoDanTocByIdAsync(userInfo.Dantoc);

            var userResponse = new UserResponse
            {
                Id = userInfo.Id,
                Hoten = userInfo.Hoten,
                Socmnd = userInfo.Socmnd,
                SDT = userInfo.Sdt,
                Ngaysinh = userInfo.Ngaysinh.Value,
                Phai = userInfo.Phai == true ? "Nam" : "Nữ",
                QuocTich = quocGia.Ten,
                DanToc = dantoc.Dantoc,
                TinhThanh = tinhthanh.Tentinhthanh,
                PhuongXa = phuongxa.Tenphuongxa
            };

            return new APIResponse<UserResponse>(success: true, message: "Lấy thông tin user thành công", data:  userResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Lỗi trong quá trình lấy thông tin tài khoản");

            return new APIResponse<UserResponse>(
                success: false,
                message: $"Đã xảy ra lỗi trong quá trình lấy thông tin tài khoản",
                data: null
            );
        }
    }
}
