using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMG.Fab.Web.Models
{
    public class PagedData<T>
    {
        public List<T> Data { get; set; }

        public int TotalElements { get; set; }
    }
}
