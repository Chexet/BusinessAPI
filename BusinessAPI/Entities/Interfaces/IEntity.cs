using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Entities.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
