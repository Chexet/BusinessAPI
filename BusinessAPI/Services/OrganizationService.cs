using AutoMapper;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Entities;
using BusinessAPI.Repositories;
using BusinessAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services
{
    public class OrganizationService
    {
        private readonly IOrganizationService _service;
        private readonly OrganizationRepository _repository;
        private readonly IMapper _mapper;

        public OrganizationService(IOrganizationService service, OrganizationRepository repository)
        {
            _service = service;
            _repository = repository;

            // var config = new MapperConfiguration();
        }

        public async Task<List<OrganizationEntity>> Get(OrganizationQuery query)
        {
            return await _repository.Get(query);
        }
    }
}
