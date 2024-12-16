using EnterpriseEntityLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseEntityLayer.Models
{
    public class AuthSession
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
    }
}
