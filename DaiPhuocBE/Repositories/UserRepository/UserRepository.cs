using DaiPhuocBE.Data;
using DaiPhuocBE.Models.Master;

namespace DaiPhuocBE.Repositories.UserRepository
{
    public class UserRepository(MasterDbContext context) : RepositoryBase<User>(context), IUserRepository
    {
        public async Task<User?> GetUserBySocmndAsync(string socmnd)
        {
            return await FirstOrDefaultAsync(u => u.Socmnd.Equals(socmnd));
        }

        public async Task<bool> IsSocmndExistsAsync(string socmnd)
        {
            return await AnyAsync(u => u.Socmnd.Equals(socmnd));
        }
    }
}
