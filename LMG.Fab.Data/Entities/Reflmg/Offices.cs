using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class Offices
    {
        public string Nom { get; set; }
        public DateTime? Office { get; set; }
        public DateTime? DossierBleu { get; set; }
        public DateTime? LimiteLivraison { get; set; }
        public DateTime? Facturation { get; set; }
        public DateTime? MiseEnVente { get; set; }
        public DateTime DateCreation { get; set; }
        public int PkOffices { get; set; }
        public string CortexCulture { get; set; }
        public string CodeOfficePapierItf { get; set; }
        public string CodeOfficeNumItf { get; set; }
    }
}
