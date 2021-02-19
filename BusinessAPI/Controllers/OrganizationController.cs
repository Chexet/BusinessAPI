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
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _service;

        public OrganizationController(IOrganizationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrganizationModel>>> Get([FromQuery]OrganizationQuery query)
        {
            var response = await _service.Get(query);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create([FromBody]OrganizationRequest request)
        {
            var model = await _service.Create(request);

            return Ok(model);
        }

        [HttpPut]
        public async Task<ActionResult> Update(OrganizationRequest request)
        {
            var model = await _service.Update(request);

            return Ok(model);
        }
    }
}
