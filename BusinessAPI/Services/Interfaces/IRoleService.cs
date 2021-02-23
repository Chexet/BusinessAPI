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
    public interface IRoleService
    {
        Task<RoleModel> Get(Guid id);
        Task<List<RoleModel>> Get(RoleQuery query);
        Task<ResponseModel<RoleModel>> Create(RoleRequest request);
        Task<ResponseModel<RoleModel>> Update(Guid id, RoleRequest request);
        Task<ResponseModel<bool>> Delete(Guid id);
    }
}
