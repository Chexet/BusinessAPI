using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Models;
using BusinessAPI.Contracts.Queries;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Contracts.Response;
using BusinessAPI.Controllers;
using BusinessAPI.Entities;
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
    public class TeamControllerTests : IntegrationTest
    {
        public IServiceScope ServiceScope { get; set; }
        private TeamController _controller;
        private BusinessContext _context;


        [SetUp]
        public void Initialize()
        {
            ServiceScope = serviceProvider.CreateScope();
            _controller = ServiceScope.ServiceProvider.GetService<TeamController>();
            _context = ServiceScope.ServiceProvider.GetService<BusinessContext>();
            SeedData(_context);
        }


        [TearDown]
        public void CleanUp()
        {
            Dispose();
        }

        [TestCase]
        public async Task Team_Get_EmptyQuery_ReturnsAllEntities()
        {
            var query = new TeamQuery();

            var actionResponse = await _controller.Get(query);
            var res = actionResponse.Result as OkObjectResult;

            var actual = res.Value as List<TeamModel>;

            Assert.IsTrue(actual.Count == 2);
        }

        [TestCase]
        public async Task Team_Get_InvalidQuery_ReturnsEmptyList()
        {
            var query = new TeamQuery() { Name = "asdasdasdwzdfsdc" };

            var actionResponse = await _controller.Get(query);
            var res = actionResponse.Result as OkObjectResult;

            var actual = res.Value as List<TeamModel>;

            Assert.IsTrue(actual.Count == 0);
        }

        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        public async Task Team_Update_UpdateUserListWithDuplicateIds_ReturnsErrorMessage(string id)
        {
            var teamReq = new TeamRequest() { UserIds = new List<Guid>() { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab1"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab1") } };

            var actionResponse = await _controller.Update(new Guid(id), teamReq);
            var res = actionResponse.Result as OkObjectResult;

            var actual = res.Value as ResponseModel<TeamModel>;

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("There are duplicate ids", actual.Errors.FirstOrDefault());
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        public async Task Team_Update_UpdateUserList_ReturnsUpdatedUserList(string id)
        {
            var teamReq = new TeamRequest() { UserIds = new List<Guid>() { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab1"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab2") } };

            var actionResponse = await _controller.Update(new Guid(id), teamReq);
            var res = actionResponse.Result as OkObjectResult;

            var actual = res.Value as ResponseModel<TeamModel>;

            Assert.IsTrue(actual.Success);
            Assert.IsTrue(actual.Data.Users.Count == 2);
            Assert.IsTrue(teamReq.UserIds.All(x => actual.Data.Users.Any(y => y.Id == x)));
        }

        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        public async Task Team_Update_UpdateUserListWithInvalidId_ReturnsErrorMessage(string id)
        {
            var teamReq = new TeamRequest() { UserIds = new List<Guid>() { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab7"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab2") } };

            var actionResponse = await _controller.Update(new Guid(id), teamReq);
            var res = actionResponse.Result as OkObjectResult;

            var actual = res.Value as ResponseModel<TeamModel>;

            Assert.IsFalse(actual.Success);
            Assert.AreEqual("Could not find provided User(s)", actual.Errors.FirstOrDefault());
        }

    }

}
