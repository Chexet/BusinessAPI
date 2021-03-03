using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Contracts.Response;
using BusinessAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAPI.IntegrationsTests.Controllers
{
    [TestFixture]
    public class OrganizationControllerTests : IntegrationTest
    {
        public IServiceScope ServiceScope { get; set; }
        private OrganizationController _controller;
        private BusinessContext _context;


        [SetUp]
        public void Initialize()
        {
            ServiceScope = serviceProvider.CreateScope();
            _controller = ServiceScope.ServiceProvider.GetService<OrganizationController>();
            _context = ServiceScope.ServiceProvider.GetService<BusinessContext>();
            SeedData(_context);
        }


        [TearDown]
        public void CleanUp()
        {
            Dispose();
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaac")]
        public async Task Organization_Get_ByExistingId_ReturnsExistingOrganization(string id)
        {
            var actionResult = await _controller.Get(new Guid(id));
            var res = actionResult.Result as OkObjectResult;

            var actual = res.Value as ResponseModel<OrganizationModel>;

            Assert.AreEqual(id, actual.Data.Id.ToString());
            Assert.IsTrue(actual.Success);
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2")]
        public async Task Organization_Get_WithNonExistingId_ReturnsErrorResponse(string id)
        {
            var actionResult = await _controller.Get(new Guid(id));
            var res = actionResult.Result as OkObjectResult;

            var actual = res.Value as ResponseModel<OrganizationModel>;

            Assert.AreEqual("No Organization found", actual?.Errors.FirstOrDefault());
            Assert.IsFalse(actual.Success);
        }


        [TestCase("n")]
        [TestCase("o")]
        public async Task Organization_Get_QueryByName_ReturnsOrganizationsContainingString(string name)
        {
            var query = new OrganizationQuery() { Name = name };
            var actionResult = await _controller.Get(query);
            var res = actionResult.Result as OkObjectResult;

            var actual = res.Value as List<OrganizationModel>;

            Assert.IsTrue(actual.Count == 2);
            Assert.IsTrue(actual.All(x => x.Name.Contains(name)));
        }


        [TestCase("Avanza")]
        [TestCase("Zalando")]
        public async Task Organization_Create_CreateOrganization_ReturnsCreatedOrganization(string name)
        {
            var orgReq = new OrganizationRequest() { Name = name };

            var actionResult = await _controller.Create(orgReq);
            var res = actionResult.Result as OkObjectResult;

            var actual = res.Value as ResponseModel<OrganizationModel>;

            Assert.AreEqual(name, actual.Data.Name);
            Assert.IsTrue(actual.Success);
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa", "Knowee")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab", "Doormie")]
        public async Task Organization_Update_NameChange_ReturnsOrgWithUpdatedName(string id, string name)
        {
            var orgReq = new OrganizationRequest() { Name = name };

            var actionResult = await _controller.Update(new Guid(id), orgReq);
            var res = actionResult.Result as OkObjectResult;

            var actual = res.Value as ResponseModel<OrganizationModel>;

            Assert.IsTrue(actual.Success);
            Assert.AreEqual(name, actual.Data.Name);
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaac")]
        public async Task Organization_Delete_DeleteOrganization_ReturnsSuccessfulStatus(string id)
        {
            var actionResult = await _controller.Delete(new Guid(id));
            var res = actionResult as OkObjectResult;

            var actual = res.Value as ResponseModel<bool>;

            Assert.IsTrue(actual.Success);
            Assert.IsNull(_context.Organizations.FirstOrDefault(x => x.Id == new Guid(id)));
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaae")]
        public async Task Organization_Delete_DeleteNonExistingId_ReturnsErrorMessage(string sid)
        {
            var id = new Guid(sid);
            var actionResult = await _controller.Delete(id);
            var res = actionResult as OkObjectResult;

            var actual = res.Value as ResponseModel<bool>;

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("No Organization with the corresponding id was found", actual.Errors.FirstOrDefault());
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task Organization_Delete_DeleteRemovesDependents_ReturnsSuccessfully(string sid)
        {
            var id = new Guid(sid);

            var actionResult = await _controller.Delete(id);
            var res = actionResult as OkObjectResult;
            var actual = res.Value as ResponseModel<bool>;

            Assert.IsTrue(actual.Success);
            Assert.IsNull(_context.Users.FirstOrDefault(x => x.OrganizationId == id));
            Assert.IsNull(_context.Teams.FirstOrDefault(x => x.OrganizationId == id));
        }
    }
}
