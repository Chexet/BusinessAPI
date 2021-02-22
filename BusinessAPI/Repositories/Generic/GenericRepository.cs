using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Response;
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
            return await queryable.Skip(query.PageSize * query.PageNumber).Take(query.PageSize).ToListAsync();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            var currTime = DateTime.UtcNow;
            entity.Created = currTime;
            entity.Updated = currTime;

            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            
            return await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
        }

        public virtual async Task<TEntity> Update(Guid id, TEntity entity)
        {
            var currEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

            if (currEntity == null) return null; 

            entity.Id = currEntity.Id;
            entity.Updated = DateTime.Now;
            entity.Created = currEntity.Created;

            _context.Update(currEntity).CurrentValues.SetValues(entity);
            return await _context.SaveChangesAsync() > 0
                ? await dbSet.FirstOrDefaultAsync(x => x.Id == id)
                : null;
        }

        public virtual async Task<ResponseModel<bool>> Delete(Guid id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return new ResponseModel<bool>(false, $"No {nameof(TEntity).Replace("Entity", "")} with the corresponding id was found");

            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0
                ? new ResponseModel<bool>(true, true)
                : new ResponseModel<bool>(false, $"Something went wrong attempting to delete {nameof(TEntity).Replace("Entity", "")}");
        }


        protected abstract IQueryable<TEntity> AddFilters(IQueryable<TEntity> queryable, TQuery query);
    }
}
