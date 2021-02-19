using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
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
        Task<TeamModel> Create(TeamRequest request);
        Task<TeamModel> Update(Guid id, TeamRequest request);
        Task Delete(Guid id);
    }
}
