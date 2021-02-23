using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Repositories
{
    public class UserRepository : GenericRepository<UserEntity, UserQuery>
    {
        public UserRepository(BusinessContext context) : base(context) { }

        protected override IQueryable<UserEntity> AddFilters(IQueryable<UserEntity> queryable, UserQuery query)
        {
            if (query.OrgId.HasValue)
                queryable = queryable.Where(x => x.OrganizationId == query.OrgId);

            if (!string.IsNullOrWhiteSpace(query.FirstName))
                queryable = queryable.Where(x => x.FirstName.ToUpper().Contains(query.FirstName.ToUpper()));

            if (!string.IsNullOrWhiteSpace(query.LastName))
                queryable = queryable.Where(x => x.LastName.ToUpper().Contains(query.LastName.ToUpper()));

            if (!string.IsNullOrWhiteSpace(query.Email))
                queryable = queryable.Where(x => x.Email.ToUpper().Contains(query.Email.ToUpper()));

            return queryable;
        }
    }
}
