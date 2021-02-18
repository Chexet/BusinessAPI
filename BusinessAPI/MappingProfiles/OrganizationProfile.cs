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
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<OrganizationRequest, OrganizationEntity>();
            CreateMap<OrganizationEntity, OrganizationModel>();
        }
    }
}
