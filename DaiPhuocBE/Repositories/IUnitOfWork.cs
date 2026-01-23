using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories.UserRepository;

namespace DaiPhuocBE.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IRepositoryBase<Btdbn> BtdbnRepository { get; }
        public IRepositoryBase<Tinhthanh> TinhThanhRepository { get; }
        public IRepositoryBase<Phuongxa> PhuongXaRepository { get; }
        public IRepositoryBase<Btddt> DanTocRepository { get; }
        public IRepositoryBase<Dmquocgium> DmQuocGiaRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
