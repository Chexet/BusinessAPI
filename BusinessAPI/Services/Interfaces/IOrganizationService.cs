using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationModel> Get(Guid id);
        Task<List<OrganizationModel>> Get(OrganizationQuery query);
        Task<OrganizationModel> Create(OrganizationRequest request);
        Task<OrganizationModel> Update(Guid id, OrganizationRequest request);
        Task Delete(Guid id);
    }
}
