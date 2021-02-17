using AutoMapper;
using BusinessAPI.Contracts.Models;
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
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationService _service;
        private readonly OrganizationRepository _repository;
        private readonly IMapper _mapper;

        public OrganizationService(IMapper mapper, IOrganizationService service, OrganizationRepository repository)
        {
            _service = service;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrganizationModel>> Get(OrganizationQuery query)
        {
            var orgs = await _repository.Get(query);

            var response = _mapper.Map<List<OrganizationEntity>, List<OrganizationModel>>(orgs);

            return response;
        }
    }
}
