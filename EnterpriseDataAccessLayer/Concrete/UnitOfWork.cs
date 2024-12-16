using EnterpriseDataAccessLayer.Abstract;
using EnterpriseDataAccessLayer.AppDbContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace EnterpriseDataAccessLayer.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EnterpriseContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(EnterpriseContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = _context.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error saving changes", ex);
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error saving changes", ex);
            }
        }

        public void Commit()
        {
            try
            {
                _transaction?.Commit();
                _transaction = null; 
            }
            catch (Exception ex)
            {
                Rollback(); 
                throw new Exception("Error committing transaction", ex);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                    _transaction = null; 
                }
            }
            catch (Exception ex)
            {
                await RollbackAsync();
                throw new Exception("Error committing transaction", ex);
            }
        }
        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
                _transaction = null; 
            }
            catch (Exception ex)
            {
                throw new Exception("Error rolling back transaction", ex);
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                    _transaction = null;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Error rolling back transaction", ex);
            }
        }
        public void Dispose()
        {
            try
            {
                _transaction?.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error disposing transaction", ex);
            }
        }
    }

}
