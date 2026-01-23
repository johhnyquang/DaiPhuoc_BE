using DaiPhuocBE.Models.Master;

namespace DaiPhuocBE.Repositories.UserRepository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserBySocmndAsync(string socmnd);
        Task<bool> IsSocmndExistsAsync(string socmnd);
    }
}
