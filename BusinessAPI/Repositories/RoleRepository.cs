using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Repositories
{
    public class RoleRepository : GenericRepository<RoleEntity, RoleQuery>
    {
        public RoleRepository(BusinessContext context) : base(context) 
        {
            dbSet = context.Set<RoleEntity>();
        }

        protected override IQueryable<RoleEntity> AddFilters(IQueryable<RoleEntity> queryable, RoleQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Name))
                queryable = queryable.Where(x => x.Name.ToUpper().Contains(query.Name.ToUpper()));

            return queryable;
        }
    }
}
