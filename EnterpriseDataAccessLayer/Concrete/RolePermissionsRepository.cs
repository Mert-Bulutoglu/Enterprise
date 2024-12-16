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
    public class RolePermissionsRepository : GenericRepository<RolePermission>, IRolePermissionsRepository
    {
        public RolePermissionsRepository(EnterpriseContext ctx) : base(ctx)
        {
        }

        public List<Permission> GetPermissionsByRoleId(int roleId)
        {
            return _ctx.RolePermissions.Where(rp => rp.RoleId == roleId).Select(rp => rp.Permission).ToList();
        }
    }
}
