using BusinessAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contexts
{
    public class BusinessContext : DbContext
    {
        public BusinessContext(DbContextOptions<BusinessContext> settings) : base(settings) { }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserEntity>()
                .HasOne(x => x.Role)
                .WithMany(y => y.Users)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserEntity>()
                .HasOne(x => x.Organization)
                .WithMany(x => x.Users)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TeamEntity>()
                .HasOne(x => x.Organization)
                .WithMany(x => x.Teams);

            base.OnModelCreating(builder);
        }
    }
}
