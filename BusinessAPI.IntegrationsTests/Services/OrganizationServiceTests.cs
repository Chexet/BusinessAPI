using BusinessAPI.Contexts;
using BusinessAPI.Repositories;
using BusinessAPI.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAPI.IntegrationsTests.Services
{
    [TestFixture]
    public class OrganizationServiceTests : IntegrationTest
    {
        public IServiceScope ServiceScope { get; set; }
        private IOrganizationService _service;
        private OrganizationRepository _repository;
        private BusinessContext _context;

        [SetUp]
        public void Initialize()
        {
            ServiceScope = serviceProvider.CreateScope();
            _service = ServiceScope.ServiceProvider.GetService<IOrganizationService>();
            _repository = ServiceScope.ServiceProvider.GetService<OrganizationRepository>();
            _context = serviceProvider.GetService<BusinessContext>();
            SeedData();
        }

        [TearDown]
        public void CleanUp()
        {
            Dispose();
        }

        private void SeedData()
        {
            throw new NotImplementedException();
        }
    }
}
