using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Models
{
    public class OrganizationModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }


        public ICollection<UserModel> Users { get; set; }
        public ICollection<TeamModel> Teams { get; set; }
        public ICollection<RoleModel> Roles { get; set; }
    }
}