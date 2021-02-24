using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Response;
using BusinessAPI.Entities;
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

        public virtual async Task<ResponseModel<TEntity>> Get(Guid id)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
                return new ResponseModel<TEntity>(false, $"No {typeof(TEntity).Name.Replace("Entity", "")} found");

            return new ResponseModel<TEntity>(entity, true);
        }

        public virtual async Task<ResponseModel<IEnumerable<TEntity>>> Get(IEnumerable<Guid> ids)
        {
            var entities = await dbSet.Where(x => ids.Contains(x.Id)).ToListAsync();

            if (entities.Count < ids.Count())
                return new ResponseModel<IEnumerable<TEntity>>(false, $"Could not find provided {typeof(TEntity).Name.Replace("Entity", "")}(s)");

            return new ResponseModel<IEnumerable<TEntity>>(entities, true);
        }

        public virtual async Task<List<TEntity>> Get(TQuery query)
        {
            var queryable = dbSet;
            queryable = AddFilters(queryable, query);
            return await queryable.Skip(query.PageSize * query.PageNumber).Take(query.PageSize).ToListAsync();
        }

        public virtual async Task<ResponseModel<TEntity>> Create(TEntity entity)
        {
            var currTime = DateTime.UtcNow;
            entity.Created = currTime;
            entity.Updated = currTime;

            await _context.Set<TEntity>().AddAsync(entity);
            var saves = await _context.SaveChangesAsync();

            var createdEntity = await dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

            return saves > 0 
                ? new ResponseModel<TEntity>(createdEntity, true)
                : new ResponseModel<TEntity>(false, $"Something went wrong when creating {typeof(TEntity).Name.Replace("Entity", "")}");
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
                return new ResponseModel<bool>(false, $"No {typeof(TEntity).Name.Replace("Entity", "")} with the corresponding id was found");

            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0
                ? new ResponseModel<bool>(true, true)
                : new ResponseModel<bool>(false, $"Something went wrong attempting to delete {typeof(TEntity).Name.Replace("Entity", "")}");
        }


        protected abstract IQueryable<TEntity> AddFilters(IQueryable<TEntity> queryable, TQuery query);
    }
}
