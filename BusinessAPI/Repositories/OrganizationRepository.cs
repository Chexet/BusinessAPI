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
    public class OrganizationRepository : GenericRepository<OrganizationEntity, OrganizationQuery>
    {
        public OrganizationRepository(BusinessContext context) : base(context)
        {
            dbSet = context.Organizations.AsQueryable();
        }

        protected override IQueryable<OrganizationEntity> AddFilters(IQueryable<OrganizationEntity> queryable, OrganizationQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Name))
                queryable = queryable.Where(x => x.Name.ToUpper().Contains(query.Name.ToUpper()));

            return queryable;
        }
    }
}
