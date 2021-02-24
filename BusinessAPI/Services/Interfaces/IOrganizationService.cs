using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<ResponseModel<OrganizationModel>> Get(Guid id);
        Task<List<OrganizationModel>> Get(OrganizationQuery query);
        Task<ResponseModel<OrganizationModel>> Create(OrganizationRequest request);
        Task<ResponseModel<OrganizationModel>> Update(Guid id, OrganizationRequest request);
        Task<ResponseModel<bool>> Delete(Guid id);
    }
}
