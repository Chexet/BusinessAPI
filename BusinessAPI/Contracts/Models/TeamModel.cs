using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Models
{
    public class TeamModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserModel> Users { get; set; }
    }
}
