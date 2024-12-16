using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseDataAccessLayer.Abstract
{
    public interface IRolePermissionsRepository : IGenericRepository<RolePermission>   
    {
        List<Permission> GetPermissionsByRoleId(int roleId);
    }
}
