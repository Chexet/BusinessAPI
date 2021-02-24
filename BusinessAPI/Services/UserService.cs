using AutoMapper;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Contracts.Response;
using BusinessAPI.Entities;
using BusinessAPI.Repositories;
using BusinessAPI.Services.Generic;
using BusinessAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services
{
    public class UserService : GenericService<UserRepository, UserEntity, UserModel, UserRequest, UserQuery>, IUserService
    {
        private readonly TeamRepository _teamRepository;

        public UserService(IMapper mapper, UserRepository repository, TeamRepository teamRepository) : base(mapper, repository) 
        {
            _teamRepository = teamRepository;    
        }

        public override async Task<ResponseModel<UserModel>> Create(UserRequest request)
        {
            // ....?
            var teamEntities = await _teamRepository.Get(request.Teams);

            if (!teamEntities.Success)
                return new ResponseModel<UserModel>(false, teamEntities.Errors[0]);

            var mapping = _mapper.Map<UserEntity>(request);
            mapping.Teams = teamEntities.Data.ToList();
            var response = await _repository.Create(mapping);

            return response.Success
                ? new ResponseModel<UserModel>(_mapper.Map<UserModel>(response.Data), true)
                : new ResponseModel<UserModel>(false, response.Errors[0]);
        }
    }
}
