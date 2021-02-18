using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<List<OrganizationModel>> Get(OrganizationQuery query);

        Task<OrganizationModel> CreateOrganization(CreateOrganizationRequest request);

    }
}
