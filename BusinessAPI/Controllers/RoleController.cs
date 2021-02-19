using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BusinessAPI.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }
    }
}
