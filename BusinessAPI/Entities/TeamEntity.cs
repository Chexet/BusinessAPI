using BusinessAPI.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Entities
{
    public class TeamEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; }
        public Guid OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public OrganizationEntity Organization { get; set; }
    }
}
