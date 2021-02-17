﻿using BusinessAPI.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Entities
{
    public class OrganizationEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoleEntity> Roles { get; set; }
        public ICollection<UserEntity> Users { get; set; }
        public ICollection<TeamEntity> Teams { get; set; }
    }
}
