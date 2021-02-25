using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Requests
{
    public class RoleRequest
    {
        public string Name { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
