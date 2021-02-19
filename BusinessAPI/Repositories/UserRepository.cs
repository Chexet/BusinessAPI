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
            throw new NotImplementedException();
        }
    }
}
