using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories.UserRepository;

namespace DaiPhuocBE.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IRepositoryBase<Btdbn> BTDBNRepository { get; }
        public IRepositoryBase<TinhThanh> TinhThanhRepository { get; }
        public IRepositoryBase<PhuongXa> PhuongXaRepository { get; }    
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
