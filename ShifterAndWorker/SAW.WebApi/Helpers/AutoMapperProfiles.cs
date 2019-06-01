using AutoMapper;
using SAW.DAL.Context;
using SAW.WebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAW.WebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<User, UserDto>();
            CreateMap<Role, UserRoleDto>();
        }

        public static void Run()
        {
            Mapper.Initialize(a =>
            {
                a.AddProfile<AutoMapperProfiles>();
            });
        }
    }
}