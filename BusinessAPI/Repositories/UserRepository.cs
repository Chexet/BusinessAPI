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
    public class UserRepository : GenericRepository<UserEntity, UserQuery>
    {
        public UserRepository(BusinessContext context) : base(context) 
        {
            dbSet = context.Set<UserEntity>().Include("Role");
        }

        protected override IQueryable<UserEntity> AddFilters(IQueryable<UserEntity> queryable, UserQuery query)
        {
            if (query.OrgId.HasValue)
                queryable = queryable.Where(x => x.OrganizationId == query.OrgId);

            if (query.RoleId.HasValue)
                queryable = queryable.Where(x => x.RoleId == query.RoleId);

            if (!string.IsNullOrWhiteSpace(query.Email))
                queryable = queryable.Where(x => x.Email.ToUpper().Contains(query.Email.ToUpper()));

            if (!string.IsNullOrWhiteSpace(query.FirstName))
                queryable = queryable.Where(x => x.FirstName.ToUpper().Contains(query.FirstName.ToUpper()));

            if (!string.IsNullOrWhiteSpace(query.LastName))
                queryable = queryable.Where(x => x.LastName.ToUpper().Contains(query.LastName.ToUpper()));

            return queryable;
        }
    }
}
