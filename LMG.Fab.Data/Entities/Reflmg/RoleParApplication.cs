using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class RoleParApplication
    {
        public int UtilisateurId { get; set; }
        public int ProfilId { get; set; }
        public int ApplicationId { get; set; }
        public string Perimetre { get; set; }

        public Application Application { get; set; }
        public NomPerimetre PerimetreNavigation { get; set; }
        public Profile Profil { get; set; }
        public Contributeur Utilisateur { get; set; }
    }
}
