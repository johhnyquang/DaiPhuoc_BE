using DaiPhuocBE.Data;
using DaiPhuocBE.Models.Master;
using DaiPhuocBE.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace DaiPhuocBE.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MasterDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(MasterDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private IUserRepository? _userRepository;
        public IUserRepository UserRepository => _userRepository ??= new UserRepository.UserRepository(_context);

        private IRepositoryBase<Btdbn>? _btdbnRepository;
        public IRepositoryBase<Btdbn> BtdbnRepository => _btdbnRepository ??= new Repositories.RepositoryBase<Btdbn>(_context);

        private IRepositoryBase<Tinhthanh>? _tinhthanhRepository;
        public IRepositoryBase<Tinhthanh> TinhThanhRepository => _tinhthanhRepository ??= new Repositories.RepositoryBase<Tinhthanh>(_context);

        private IRepositoryBase<Phuongxa>? _phuongXaRepository;
        public IRepositoryBase<Phuongxa> PhuongXaRepository => _phuongXaRepository ??= new Repositories.RepositoryBase<Phuongxa>(_context);

        private IRepositoryBase<Btddt>? _dantocRepository;
        public IRepositoryBase<Btddt> DanTocRepository => _dantocRepository ??= new Repositories.RepositoryBase<Btddt>(_context);

        private IRepositoryBase<Dmquocgium>? _dmQuocGiaRepository;
        public IRepositoryBase<Dmquocgium> DmQuocGiaRepository => _dmQuocGiaRepository ??= new Repositories.RepositoryBase<Dmquocgium>(_context);

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Transaction đã tồn tại");
            }
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Chưa có transaction nào được khởi tạo");
            }

            try
            {
                await SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
