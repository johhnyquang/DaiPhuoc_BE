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
        public IRepositoryBase<Btdbn> BTDBNRepository => _btdbnRepository ??= new Repositories.RepositoryBase<Btdbn>(_context);

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
