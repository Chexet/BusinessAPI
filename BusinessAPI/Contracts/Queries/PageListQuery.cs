using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAPI.Contracts.Queries
{
    public class PageListQuery
    {
        public string OrderBy { get; set; }
        public int PageNumber { get; set; }
        public virtual int PageSize { get; set; } = 20;
    }
}
