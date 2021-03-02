using BusinessAPI.Contexts;
using BusinessAPI.Controllers;
using BusinessAPI.Entities;
using BusinessAPI.IntegrationTests;
using BusinessAPI.Repositories;
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
                        services.AddScoped<OrganizationService>();
                        services.AddScoped<ITeamService, TeamService>();
                        services.AddScoped<UserService>();
                        services.AddScoped<TeamRepository>();
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

        protected void SeedData(BusinessContext context)
        {
            var organizations = new List<OrganizationEntity>()
            {
                new OrganizationEntity()
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Created = DateTime.Now.AddDays(-1),
                    Updated = DateTime.Now.AddDays(-1),
                    Name = "Knowe"
                },
                new OrganizationEntity()
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"),
                    Created = DateTime.Now.AddDays(-1),
                    Updated = DateTime.Now.AddDays(-1),
                    Name = "Dormy"
                },
                new OrganizationEntity()
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaac"),
                    Created = DateTime.Now.AddDays(-1),
                    Updated = DateTime.Now.AddDays(-1),
                    Name = "Avanza"
                }
            };

            var teams = new List<TeamEntity>()
            {
                new TeamEntity()
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                    Created = DateTime.Now.AddDays(-2),
                    Updated = DateTime.Now.AddDays(-1),
                    Name = "Developers",
                    OrganizationId = organizations[0].Id
                },
                new TeamEntity()
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                    Created = DateTime.Now.AddDays(-2),
                    Updated = DateTime.Now.AddDays(-1),
                    Name = "Marketing",
                    OrganizationId = organizations[0].Id
                },
            };

            var users = new List<UserEntity>()
            {
                new UserEntity()
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab1"),
                    Created = DateTime.Now.AddDays(-2),
                    Updated = DateTime.Now.AddDays(-2),
                    FirstName = "Ge",
                    LastName = "Pe",
                    Email = "gp@live.se",
                    Teams = new List<TeamEntity>() { teams[0] },
                    OrganizationId = organizations[0].Id
                },
                new UserEntity()
                {
                    Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaab2"),
                    Created = DateTime.Now.AddDays(-2),
                    Updated = DateTime.Now.AddDays(-2),
                    FirstName = "Gu",
                    LastName = "Pe",
                    Email = "gp@lajv.se",
                    Teams = new List<TeamEntity>() { teams[1] },
                    OrganizationId = organizations[0].Id
                },
            };

            context.Organizations.AddRange(organizations);
            context.Teams.AddRange(teams);
            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
