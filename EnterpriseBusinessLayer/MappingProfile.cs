using AutoMapper;
using EnterpriseEntityLayer.DTOs;
using EnterpriseEntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseBusinessLayer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<Role, RoleDto>()
                .ReverseMap();

            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Product, ProductDto>()
                .ReverseMap();

            CreateMap<Order, OrderDto>()
                .ReverseMap();
        }
    }
}
