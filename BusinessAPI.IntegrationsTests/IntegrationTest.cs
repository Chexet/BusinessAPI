using BusinessAPI.Contexts;
using BusinessAPI.Controllers;
using BusinessAPI.IntegrationTests;
using BusinessAPI.Services;
using BusinessAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAPI.IntegrationsTests
{
    public class IntegrationTest
    {
        protected readonly IServiceProvider serviceProvider;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.SetupTestDatabase<BusinessContext>("FakeInMemoryBusinessDb");
                        services.AddScoped<OrganizationController>();
                        services.AddScoped<TeamController>();
                        services.AddScoped<UserController>();
                        services.AddScoped<IOrganizationService, OrganizationService>();
                        services.AddScoped<ITeamService, TeamService>();
                        services.AddScoped<IUserService, UserService>();
                    });
                });
            serviceProvider = appFactory.Services;
        }

        public void Dispose()
        {
            using var serviceScope = serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<BusinessContext>();
            context.Database.EnsureDeleted();
        }
    }
}
