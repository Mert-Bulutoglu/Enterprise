using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseBusinessLayer.Abstract
{
    public interface IAuthService
    {
        AuthResponse Login(string username, string password);

        bool IsAuthorized(string token, string permissionName);

        bool Logout(string token, int userId);
    }
}
