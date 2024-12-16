using EnterpriseDataAccessLayer.Abstract;
using EnterpriseDataAccessLayer.AppDbContext;
using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Concrete
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(EnterpriseContext ctx) : base(ctx)
        {
        }

        public Role GetRoleById(int roleId)
        {
            return _ctx.Roles.FirstOrDefault(r => r.Id == roleId);
        }
    }
}
