using AutoMapper;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Requests;
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
                .ForMember(destination => destination.TeamIds,
                    options => options
                        .MapFrom(source => source.Teams.Select(x => x.Id).ToList()));
            CreateMap<UserRequest, UserEntity>()
                .ForSourceMember(source => source.TeamIds,
                    options => options.DoNotValidate());
        }
    }
}
