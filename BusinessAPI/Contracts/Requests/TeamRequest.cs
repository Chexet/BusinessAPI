using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Requests
{
    public class TeamRequest
    {
        public string Name { get; set; }
        public Guid OrgId { get; set; }
        public List<Guid> UserIds { get; set; }
    }
}
