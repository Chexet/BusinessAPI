﻿using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Repositories
{
    public class TeamRepository : GenericRepository<TeamEntity, TeamQuery>
    {
        public TeamRepository(BusinessContext context) : base(context)
        {
            dbSet = context.Set<TeamEntity>();
        }

        protected override IQueryable<TeamEntity> AddFilters(IQueryable<TeamEntity> queryable, TeamQuery query)
        {
            return queryable;
        }
    }
}
