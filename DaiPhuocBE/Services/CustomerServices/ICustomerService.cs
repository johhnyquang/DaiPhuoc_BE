using DaiPhuocBE.DTOs;
using DaiPhuocBE.DTOs.CustomerDTOs;

namespace DaiPhuocBE.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<APIResponse<CustomerResponse>> GetInfoCustomer(string mabn);
    }
}
