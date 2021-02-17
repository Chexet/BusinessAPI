using BusinessAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Queries
{
    public class TeamQuery : PageListQuery
    {
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
