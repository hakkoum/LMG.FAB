using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMG.Fab.Web.Models
{
    public class DbField
    {
        public string TableName { get; set; }

        public List<string> Fields { get; set; }
    }
}
