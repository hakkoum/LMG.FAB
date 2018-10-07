using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class Application
    {
        public Application()
        {
            RoleParApplication = new HashSet<RoleParApplication>();
        }

        public int PkApplication { get; set; }
        public string Nom { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string Icone { get; set; }

        public ICollection<RoleParApplication> RoleParApplication { get; set; }
    }
}
