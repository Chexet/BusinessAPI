using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Response;
using BusinessAPI.Entities;
using Microsoft.EntityFrameworkCore;
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
            dbSet = context.Set<TeamEntity>().Include(e => e.Users);
        }

        public override async Task<TeamEntity> Update(Guid id, TeamEntity entity)
        {
            var currEntity = await _context.Set<TeamEntity>().Include(e => e.Users).FirstOrDefaultAsync(x => x.Id == id);
            if (currEntity == null) return null;

            entity.Id = currEntity.Id;
            entity.Updated = DateTime.Now;
            entity.Created = currEntity.Created;
            currEntity.Users = entity.Users;

            _context.Update(currEntity).CurrentValues.SetValues(entity);
            return await _context.SaveChangesAsync() > 0
                ? await dbSet.FirstOrDefaultAsync(x => x.Id == id)
                : null;
        }

        protected override IQueryable<TeamEntity> AddFilters(IQueryable<TeamEntity> queryable, TeamQuery query)
        {
            if (query.OrgId.HasValue)
                queryable = queryable.Where(x => x.OrganizationId == query.OrgId);

            if (!string.IsNullOrWhiteSpace(query.Name))
                queryable = queryable.Where(x => x.Name.ToUpper().Contains(query.Name.ToUpper()));

            return queryable;
        }
    }
}
