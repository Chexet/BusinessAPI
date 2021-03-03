using BusinessAPI.Contexts;
using BusinessAPI.Contracts.Requests;
using BusinessAPI.Entities;
using BusinessAPI.IntegrationsTests;
using BusinessAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAPI.IntegrationTests.Repositories
{
    public class TeamRepositoryTests : IntegrationTest
    {
        public IServiceScope ServiceScope { get; set; }
        private TeamRepository _repository;
        private BusinessContext _context;


        [SetUp]
        public void Initialize()
        {
            ServiceScope = serviceProvider.CreateScope();
            _repository = ServiceScope.ServiceProvider.GetService<TeamRepository>();
            _context = ServiceScope.ServiceProvider.GetService<BusinessContext>();
            SeedData(_context);
        }


        [TearDown]
        public void CleanUp()
        {
            Dispose();
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        public async Task Team_Get_GetExistingTeamById_ReturnsExistingTeam(string sid)
        {
            var id = new Guid(sid);

            var response = await _repository.Get(id);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(id, response.Data.Id);
        }


        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa6")]
        public async Task Team_Get_InvalidId_ReturnsErrorMessage(string sid)
        {
            var id = new Guid(sid);

            var response = await _repository.Get(id);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("No Team found", response.Errors.FirstOrDefault());
        }

        [TestCase]
        public async Task Team_Get_DuplicateIds_ReturnsErrorMessage()
        {
            var ids = new List<Guid>() { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa6"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa6") };

            var response = await _repository.Get(ids);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("There are duplicate ids", response.Errors.FirstOrDefault());
        }

        [TestCase]
        public async Task Team_Get_ListWithInvalidId_ReturnsErrorMessage()
        {
            var ids = new List<Guid>() { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa6") };

            var response = await _repository.Get(ids);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Could not find provided Team(s)", response.Errors.FirstOrDefault());
        }

        [TestCase("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2")]
        public async Task Team_Get_ListWithValidIds_ReturnsListOfEntities(params string[] idArr)
        {
            var response = await _repository.Get(Array.ConvertAll(idArr, Guid.Parse));

            Assert.IsTrue(response.Success);
            Assert.IsTrue(response.Data.Count() == idArr.Length);
        }

        [TestCase("GAB", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")]
        public async Task Team_Create_ValidRequest_ReturnsCreatedEntity(string name, string sid)
        {
            var orgId = new Guid(sid);
            var requestEntity = new TeamEntity()
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Name = name,
                OrganizationId = orgId
            };

            var response = await _repository.Create(requestEntity);

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(_context.Teams.FirstOrDefault(x => x.Id == requestEntity.Id));
        }

        [TestCase("GAB", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaad")]
        public async Task Team_Create_RequestWithInvalidOrgId_ReturnsErrorMessage(string name, string orgId)
        {
            var id = new Guid(orgId);
            var requestEntity = new TeamEntity()
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Name = name,
                OrganizationId = id
            };

            var response = await _repository.Create(requestEntity);

            Assert.IsNull(_context.Teams.FirstOrDefault(x => x.Id == requestEntity.Id));
        }


        [TestCase("Administration", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaac", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2")]
        [TestCase("Trading", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1")]
        public async Task Team_Update_ValidChanges_ReturnsTeamEntity(string name, string orgId, string teamId)
        {
            var oid = new Guid(orgId);
            var tid = new Guid(teamId);
            var teamEntity = _context.Teams.FirstOrDefault(x => x.Id == tid);
            teamEntity.OrganizationId = oid;
            teamEntity.Name = name;

            var response = await _repository.Update(tid, teamEntity);

            Assert.IsNotNull(response);
            Assert.AreEqual(oid, response.OrganizationId);
            Assert.AreEqual(name, response.Name);
        }

    }
}
