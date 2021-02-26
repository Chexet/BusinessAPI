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
    public class TeamService : GenericService<TeamRepository, TeamEntity, TeamModel, TeamRequest, TeamQuery>, ITeamService
    {
        private readonly UserRepository userRepository;
        public TeamService(IMapper mapper, TeamRepository teamRepository, UserRepository repository) : base(mapper, teamRepository) 
        {
            userRepository = repository;
        }

        public override async Task<ResponseModel<TeamModel>> Create(TeamRequest request)
        {
            var userEntities = await userRepository.Get(request.UserIds);

            if (!userEntities.Success)
                return new ResponseModel<TeamModel>(false, userEntities.Errors[0]);

            var mapping = _mapper.Map<TeamEntity>(request);
            mapping.Users = userEntities.Data.ToList();
            var response = await _repository.Create(mapping);

            return response.Success
                ? new ResponseModel<TeamModel>(_mapper.Map<TeamModel>(response.Data), true)
                : new ResponseModel<TeamModel>(false, response.Errors[0]);
        }

        public override async Task<ResponseModel<TeamModel>> Update(Guid id, TeamRequest request)
        {
            var userEntities = await userRepository.Get(request.UserIds);

            if (!userEntities.Success)
                return new ResponseModel<TeamModel>(false, userEntities.Errors.FirstOrDefault());

            var entity = _mapper.Map<TeamEntity>(request);
            entity.Users = userEntities.Data.ToList();

            var updatedEntity = await _repository.Update(id, entity);

            if (updatedEntity == null)
                return new ResponseModel<TeamModel>(false, "Something went wrong");

            return new ResponseModel<TeamModel>(_mapper.Map<TeamModel>(updatedEntity), true);
        }
    }
}
