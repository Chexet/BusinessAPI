using AutoMapper;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Entities;
using BusinessAPI.Repositories;
using BusinessAPI.Services.Interfaces;
using BusinessAPI.Services.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services
{
    public class OrganizationService : GenericService<OrganizationRepository, OrganizationEntity, OrganizationModel, OrganizationRequest, OrganizationQuery> , IOrganizationService
    {
        public OrganizationService(IMapper mapper, OrganizationRepository repository) : base(mapper, repository) { }

        
    }
}
