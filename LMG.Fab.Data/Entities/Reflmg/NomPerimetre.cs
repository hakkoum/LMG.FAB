using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class NomPerimetre
    {
        public NomPerimetre()
        {
            Perimetre = new HashSet<Perimetre>();
            RoleParApplication = new HashSet<RoleParApplication>();
        }

        public string Nomperimetre { get; set; }

        public ICollection<Perimetre> Perimetre { get; set; }
        public ICollection<RoleParApplication> RoleParApplication { get; set; }
    }
}
