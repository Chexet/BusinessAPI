using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Services;
using BusinessAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAPI.IntegrationsTests.Services
{
    [TestFixture]
    public class TeamServiceTests : IntegrationTest
    {
        public IServiceScope ServiceScope { get; set; }
        private BusinessContext _context;
        private ITeamService _service;

        [SetUp]
        public void Initialize()
        {
            ServiceScope = serviceProvider.CreateScope();
            _context = ServiceScope.ServiceProvider.GetService<BusinessContext>();
            _service = ServiceScope.ServiceProvider.GetService<ITeamService>();
            SeedData(_context);
        }

        [TearDown]
        public void CleanUp()
        {
            Dispose();
        }

        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2")]
        public async Task Team_Get_ByExistingId_ReturnsExistingUserResponseModel(string id)
        {
            var response = await _service.Get(new Guid(id));

            Assert.IsTrue(response.Success);
            Assert.AreEqual(new Guid(id), response.Data.Id);
        }

        [TestCase("Finance", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        [TestCase("Administration", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab")]
        public async Task Team_Create_CreateTeam_ReturnsCreatedTeam(string name, string orgId)
        {
            var teamReq = new TeamRequest() { Name = name, OrgId = new Guid(orgId), UserIds = new List<Guid>() };
            var response = await _service.Create(teamReq);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(name, response.Data.Name);
            Assert.IsNotNull(_context.Teams.FirstOrDefault(x => x.Name == name));
        }

        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2")]
        public async Task Team_Delete_DeleteTeam_ReturnsSuccessResponse(string sid)
        {
            var id = new Guid(sid);
            var response = await _service.Delete(id);

            Assert.IsTrue(response.Success);
            Assert.IsNull(_context.Teams.FirstOrDefault(x => x.Id == id));
        }
    }
}
