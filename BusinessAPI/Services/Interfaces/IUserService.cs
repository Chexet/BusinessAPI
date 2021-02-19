﻿using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
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
        Task<UserModel> Create(UserRequest request);
        Task<UserModel> Update(Guid id, UserRequest request);
        Task Delete(Guid id);
    }
}
