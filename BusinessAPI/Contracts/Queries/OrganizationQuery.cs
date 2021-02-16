using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Queries
{
    public class OrganizationQuery : PageListQuery
    {
        public string Name { get; set; }
    }
}
