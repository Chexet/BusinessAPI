using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Contracts.Response;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleModel>> Get([FromRoute] Guid id)
        {
            var response = await _service.Get(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleModel>>> Get([FromQuery] RoleQuery query)
        {
            var response = await _service.Get(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<RoleModel>>> Create([FromBody] RoleRequest request)
        {
            var model = await _service.Create(request);

            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<RoleModel>>> Update([FromQuery] Guid id, [FromBody] RoleRequest request)
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
