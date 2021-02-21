using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<UserModel>> Get([FromQuery] Guid id)
        {
            var response = await _service.Get(id);

            return Ok(response);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<UserModel>>> Get([FromQuery] UserQuery query)
        {
            var response = await _service.Get(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create([FromBody] UserRequest request)
        {
            var model = await _service.Create(request);

            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromQuery] Guid id, [FromBody] UserRequest request)
        {
            var model = await _service.Update(id, request);

            return Ok(model);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] Guid id)
        {
            await _service.Delete(id);

            return Ok();
        }
    }
}
