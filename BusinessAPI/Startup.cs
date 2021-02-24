using BusinessAPI.Contexts;
using BusinessAPI.Repositories;
using BusinessAPI.Services;
using BusinessAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessAPI.Installers.Extensions;

namespace BusinessAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(cfg => cfg.AllowNullCollections = true, typeof(Startup));
            services.AddSwaggerGen();

            services.AddDbContext<BusinessContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BusinessDatabase")));
            
            AddRepositories(services);
            AddServices(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDevelopmentExceptionHandling();
            else
                app.UseProdExceptionHandling();

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "BusinessAPI v1");
                config.RoutePrefix = string.Empty;
            });

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddServices(IServiceCollection services)
        {
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
        }

        private void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<OrganizationRepository>();
            services.AddScoped<TeamRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<RoleRepository>();
        }
    }
}
