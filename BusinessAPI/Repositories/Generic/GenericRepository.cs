using BusinessAPI.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Repositories
{
    public class GenericRepository<TEntity, TQuery> where TEntity : class
    {
        protected readonly BusinessContext businessContext;
        protected IQueryable<TEntity> dbSet;

        public GenericRepository(BusinessContext context)
        {
            businessContext = context;
            dbSet = businessContext.Set<TEntity>().AsQueryable();
        }
    }
}
