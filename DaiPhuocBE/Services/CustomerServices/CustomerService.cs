using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.CustomerDTOs;
using DaiPhuocBE.Repositories;

namespace DaiPhuocBE.Services.CustomerServices;

public class CustomerService (IUnitOfWork unitOfWork, ILogger<CustomerService> logger) : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<CustomerService> _logger = logger;
    public async Task<APIResponse<CustomerResponse>> GetInfoCustomer(string mabn)
    {
        if (string.IsNullOrEmpty(mabn))
        {
            return new APIResponse<CustomerResponse>(success: false, message: "Mã bệnh nhân không được trống", data: null); 
        }

        try
        {
            var customer = await _unitOfWork.BtdbnRepository.GetByIdAsync(mabn);
            if (customer == null)
            {
                return new APIResponse<CustomerResponse>(success: false, message: "Không tìm thấy thông tin bệnh nhân", data: null);
            }

            var customerResponse = new CustomerResponse
            {
                Mabn = customer.Mabn,
                HoTen = customer.Hoten ?? "Không xác định",
                NgaySinh = customer.Ngaysinh.HasValue == true ? customer.Ngaysinh.Value : DateTime.Now,
                Phai = customer.Phai ?? false,
                SoCmnd = customer.Socmnd ?? "Không xác định",
                BHYT = "BHYT"
            };

            return new APIResponse<CustomerResponse>(success: true,message: "Tìm thấy thông tin", data: customerResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Lỗi trong quá trình lấy thông tin bệnh nhân");
            return new APIResponse<CustomerResponse>(success: false, message: "Lỗi trong quá trình lấy thông tin bệnh nhân", data: null);
        }

    }
}
