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

        public async Task<List<OrganizationEntity>> Get(OrganizationQuery query)
        {
            var queryable = dbSet;
            queryable = AddFilters(queryable, query);
            return await (queryable.Skip(query.PageSize * query.PageNumber).Take(query.PageSize)).ToListAsync();
        }

        protected override IQueryable<OrganizationEntity> AddFilters(IQueryable<OrganizationEntity> queryable, OrganizationQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Name))
                queryable = queryable.Where(x => x.Name.ToUpper().Contains(query.Name.ToUpper()));

            return queryable;
        }
    }
}
