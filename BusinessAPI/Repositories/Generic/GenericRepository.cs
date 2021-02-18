using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Repositories
{
    public abstract class GenericRepository<TEntity, TQuery> where TEntity : class, IEntity where TQuery : PageListQuery
    {
        protected readonly BusinessContext _context;
        protected IQueryable<TEntity> dbSet;

        public GenericRepository(BusinessContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<TEntity> Get(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<List<TEntity>> Get(TQuery query)
        {
            var queryable = dbSet;
            queryable = AddFilters(queryable, query);
            return await (queryable.Skip(query.PageSize * query.PageNumber).Take(query.PageSize)).ToListAsync();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
        }

        protected abstract IQueryable<TEntity> AddFilters(IQueryable<TEntity> queryable, TQuery query);
    }
}
