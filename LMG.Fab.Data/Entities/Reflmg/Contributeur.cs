using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class Contributeur
    {
        public Contributeur()
        {
            RoleParApplication = new HashSet<RoleParApplication>();
        }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int PkContributeur { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Guid { get; set; }
        public string Gad { get; set; }
        public string Email { get; set; }
        public string CortexCulture { get; set; }
        public int? PrefLignes { get; set; }
        public int? PrefFormat { get; set; }
        public string ResetToken { get; set; }

        public ICollection<RoleParApplication> RoleParApplication { get; set; }
    }
}
