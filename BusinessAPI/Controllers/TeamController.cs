using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Contracts.Response;
using BusinessAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Controllers
{
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamModel>> Get([FromRoute] Guid id)
        {
            var response = await _service.Get(id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<TeamModel>>> Get([FromQuery] TeamQuery query)
        {
            var response = await _service.Get(query);

            return Ok(response);
        }

        //[HttpGet("GetAll")]
        //public async Task<ActionResult<List<TeamModel>>> Get()
        //{
        //    var response = await _service.Get();

        //    return Ok(response);
        //}

        [HttpPost]
        public async Task<ActionResult<TeamModel>> Create([FromBody] TeamRequest request)
        {
            var response  = await _service.Create(request);

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseModel<TeamModel>>> Update([FromQuery] Guid id, [FromBody] TeamRequest request)
        {
            var response = await _service.Update(id, request);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseModel<bool>>> Delete([FromQuery] Guid id)
        {
            var response = await _service.Delete(id);

            return Ok(response);
        }
    }
}
