using AutoMapper;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserModel>()
                .ForMember(destination => destination.RoleName, 
                    options => options
                        .MapFrom(source => source.Role.Name))
                .ForMember(destination => destination.Name, 
                    options => options.MapFrom(source => source.FirstName + " " + source.LastName));
        }
    }
}
