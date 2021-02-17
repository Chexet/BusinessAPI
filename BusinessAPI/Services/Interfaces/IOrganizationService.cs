using BusinessAPI.Contracts.Queries;
using BusinessAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services.Interfaces
{
    public interface IOrganizationService
    {
        Task<ICollection<OrganizationEntity>> Get(OrganizationQuery query);
    }
}
