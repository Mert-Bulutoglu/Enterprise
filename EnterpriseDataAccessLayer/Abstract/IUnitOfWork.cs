using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void BeginTransaction();
        void Commit();
        void Rollback();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
