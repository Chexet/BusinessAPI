using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Repositories
{
    public class RoleRepository : GenericRepository<RoleEntity, RoleQuery>
    {
        public RoleRepository(BusinessContext context) : base(context) { }

        protected override IQueryable<RoleEntity> AddFilters(IQueryable<RoleEntity> queryable, RoleQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
