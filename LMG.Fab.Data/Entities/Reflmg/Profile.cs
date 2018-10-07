using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class Profile
    {
        public Profile()
        {
            ProfileFonction = new HashSet<ProfileFonction>();
            RoleParApplication = new HashSet<RoleParApplication>();
        }

        public string Nom { get; set; }
        public string Description { get; set; }
        public int PkProfile { get; set; }
        public string CortexCulture { get; set; }
        public string LibelleSso { get; set; }

        public ICollection<ProfileFonction> ProfileFonction { get; set; }
        public ICollection<RoleParApplication> RoleParApplication { get; set; }
    }
}
