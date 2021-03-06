using BusinessAPI.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Entities
{
    public class UserEntity : IEntity
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


        public Guid OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public OrganizationEntity Organization { get; set; }


        public Guid? RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public RoleEntity Role { get; set; }


        public List<TeamEntity> Teams { get; set; }
    }
}
