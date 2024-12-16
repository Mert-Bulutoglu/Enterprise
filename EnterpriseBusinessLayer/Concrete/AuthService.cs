using AutoMapper;
using EnterpriseBusinessLayer.Abstract;
using EnterpriseDataAccessLayer.Abstract;
using EnterpriseEntityLayer.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnterpriseBusinessLayer.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly IMapper _mapper;
        private readonly IDatabase _redisDb;
        private readonly IConnectionMultiplexer _redis;
        public AuthService(IRolePermissionsRepository rolePermissionsRepository,
            IMapper mapper,
            IUserRepository userRepository,
            IConnectionMultiplexer redis)
        {
            _rolePermissionsRepository = rolePermissionsRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _redis = redis;
            _redisDb = redis.GetDatabase();
        }

        public bool IsAuthorized(string token, string permissionName)
        {

            var sessionData = _redisDb.StringGet($"session:{token}");
            if (string.IsNullOrEmpty(sessionData)) throw new UnauthorizedAccessException("Invalid token.");

            var session = JsonSerializer.Deserialize<AuthSession>(sessionData);

            if (session.Permissions.Contains(permissionName)) return true;

            return false;
        }

        public AuthResponse Login(string username, string password)
        {
            var user = _userRepository.GetByUsernameAndPassword(username, password);
            if (user == null) throw new UnauthorizedAccessException("Invalid username or password.");

            var permissions = _rolePermissionsRepository.GetPermissionsByRoleId(user.RoleId);
            var permissionList = permissions.Select(p => p.Name).ToList();

            var token = _redisDb.StringGet($"userId:{user.Id}");

            if (string.IsNullOrEmpty(token))
            {
                token = Guid.NewGuid().ToString();

                var authSession = new AuthSession
                {
                    UserId = user.Id,
                    RoleName = user.Role.Name,
                    Permissions = permissionList
                };

                var sessionData = JsonSerializer.Serialize(authSession);
                _redisDb.StringSet($"session:{token}", sessionData, TimeSpan.FromHours(1));

                _redisDb.StringSet($"userId:{user.Id}", token, TimeSpan.FromHours(1));
            }

            return new AuthResponse
            {
                Token = token,
                UserId = user.Id,
                RoleName = user.Role.Name,
                Permissions = permissionList
            };

        }


        public bool Logout(string token, int userId)
        {
            var sessionData = _redisDb.StringGet($"session:{token}");
            if (string.IsNullOrEmpty(sessionData))
            {
                return false;  
            }

            _redisDb.KeyDelete($"session:{token}");
            _redisDb.KeyDelete($"userId:{userId}");


            return true;
        }
    }
}
