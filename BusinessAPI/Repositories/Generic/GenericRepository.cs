using BusinessAPI.Contexts;
using BusinessAPI.Entities.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Repositories
{
    public abstract class GenericRepository<TEntity, TQuery> where TEntity : class, IEntity
    {
        protected readonly BusinessContext _context;
        protected IQueryable<TEntity> dbSet;

        public GenericRepository(BusinessContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        protected abstract IQueryable<TEntity> AddFilters(IQueryable<TEntity> queryable, TQuery query);
    }
}
