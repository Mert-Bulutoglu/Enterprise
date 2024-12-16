using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseEntityLayer.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
