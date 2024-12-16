using EnterpriseDataAccessLayer.Abstract;
using EnterpriseDataAccessLayer.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal EnterpriseContext _ctx;

        public GenericRepository(EnterpriseContext ctx)
        {
            _ctx = ctx;
        }


    }
}
