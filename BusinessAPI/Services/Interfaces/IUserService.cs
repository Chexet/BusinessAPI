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
    public interface IUserService
    {
        Task<UserModel> Get(Guid id);
        Task<List<UserModel>> Get(UserQuery query);
        Task<ResponseModel<UserModel>> Create(UserRequest request);
        Task<ResponseModel<UserModel>> Update(Guid id, UserRequest request);
        Task<ResponseModel<bool>> Delete(Guid id);
    }
}
