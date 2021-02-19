using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
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
        Task<RoleModel> Create(RoleRequest request);
        Task<RoleModel> Update(Guid id, RoleRequest request);
        Task Delete(Guid id);
    }
}
