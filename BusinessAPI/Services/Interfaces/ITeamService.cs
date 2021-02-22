using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamModel> Get(Guid id);
        Task<List<TeamModel>> Get(TeamQuery query);
        Task<ResponseModel<TeamModel>> Create(TeamRequest request);
        Task<ResponseModel<TeamModel>> Update(Guid id, TeamRequest request);
        Task Delete(Guid id);
    }
}
