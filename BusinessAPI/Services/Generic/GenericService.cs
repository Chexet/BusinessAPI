using AutoMapper;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Entities.Interfaces;
using BusinessAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services.Generic
{
    public abstract class GenericService<TRepository, TEntity, TModel, TRequest, TQuery> where TRepository : GenericRepository<TEntity, TQuery> 
        where TEntity : class, IEntity where TModel : class where TRequest : class where TQuery : PageListQuery
    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;

        public GenericService(IMapper mapper, TRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<List<TModel>> Get(TQuery query)
        {
            var orgs = await _repository.Get(query);

            var response = _mapper.Map<List<TEntity>, List<TModel>>(orgs);

            return response;
        }

        public virtual async Task<TModel> Create(TRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);
            var response = await _repository.Create(entity);

            // if (response == null) 
            // Return some response model with err 

            return _mapper.Map<TModel>(response);
        }

        public virtual async Task<TModel> Update(Guid id, TRequest request)
        {

        }
    }
}
