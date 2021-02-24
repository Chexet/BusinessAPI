using BusinessAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Requests
{
    public class UserRequest
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public List<Guid> TeamIds { get; set; }
        public Guid? OrganizationId { get; set; }
        public Guid? RoleId { get; set; }
    }
}
