using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseEntityLayer.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> PermissionNames { get; set; }
    }
}
