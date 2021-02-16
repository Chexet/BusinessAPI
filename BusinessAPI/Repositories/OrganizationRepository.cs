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
    public class OrganizationRepository
    {
        private readonly BusinessContext _context;
        protected IQueryable<OrganizationEntity> dbSet;

        public OrganizationRepository(BusinessContext context)
        {
            _context = context;
            dbSet = context.Organizations.AsQueryable();
        }

        public async Task<List<OrganizationEntity>> Get(OrganizationQuery query)
        {
            var queryable = dbSet;
            queryable = AddFilters(queryable, query);
            return await (queryable.Skip(query.PageSize - 1).Take(query.PageSize)).ToListAsync();
        }

        public IQueryable<OrganizationEntity> AddFilters(IQueryable<OrganizationEntity> queryable, OrganizationQuery query)
        {
            if (!string.IsNullOrWhiteSpace(query.Name))
                queryable = queryable.Where(x => x.Name.ToUpper().Contains(query.Name.ToUpper()));

            return queryable;
        }
    }
}
