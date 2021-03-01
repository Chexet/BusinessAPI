using BusinessAPI.Contexts;
using BusinessAPI.Controllers;
using BusinessAPI.IntegrationTests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessAPI.IntegrationsTests
{
    class IntegrationTest
    {
        protected readonly IServiceProvider serviceProvider;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.SetupTestDatabase<BusinessContext>(Guid.NewGuid().ToString());
                        services.AddScoped<OrganizationController>();
                        services.AddScoped<TeamController>();
                        services.AddScoped<UserController>();
                        services.AddScoped<RoleController>();
                    });
                });
        }

        public void Dispose()
        {
            using var serviceScope = serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<BusinessContext>();
            context.Database.EnsureDeleted();
        }
    }
}
