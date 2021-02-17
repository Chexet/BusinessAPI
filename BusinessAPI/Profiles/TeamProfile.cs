using AutoMapper;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<TeamEntity, TeamModel>();
        }
    }
}
