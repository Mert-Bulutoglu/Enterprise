using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByUsernameAndPassword(string username, string password);
        User GetById(int userId);
    }
}
