using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class Fonction
    {
        public Fonction()
        {
            ProfileFonction = new HashSet<ProfileFonction>();
        }

        public string Nom { get; set; }
        public string Description { get; set; }
        public string Redirection { get; set; }
        public string Application { get; set; }
        public int PkFonction { get; set; }
        public string CortexCulture { get; set; }
        public int? FkReference { get; set; }
        public int? PosReference { get; set; }
        public string Code { get; set; }

        public ICollection<ProfileFonction> ProfileFonction { get; set; }
    }
}
