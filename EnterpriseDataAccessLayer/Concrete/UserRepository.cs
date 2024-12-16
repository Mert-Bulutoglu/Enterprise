using EnterpriseDataAccessLayer.Abstract;
using EnterpriseDataAccessLayer.AppDbContext;
using EnterpriseEntityLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EnterpriseContext ctx) : base(ctx)
        {
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return _ctx.Users.Include(x => x.Role).FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public User GetById(int userId)
        {
            return _ctx.Users.FirstOrDefault(u => u.Id == userId);
        }
    }
}
