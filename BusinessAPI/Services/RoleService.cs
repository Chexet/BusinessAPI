using AutoMapper;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Entities;
using BusinessAPI.Repositories;
using BusinessAPI.Services.Generic;
using BusinessAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services
{
    public class RoleService : GenericService<RoleRepository, RoleEntity, RoleModel, RoleRequest, RoleQuery>, IRoleService
    {
        public RoleService(IMapper mapper, RoleRepository repository) : base(mapper, repository) { }
    }
}
