﻿using BusinessAPI.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> CreateUser { get; set; }
    }
}
