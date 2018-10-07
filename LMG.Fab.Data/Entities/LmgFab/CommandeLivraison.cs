using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class CommandeLivraison
    {
        public int PkCommandeLivraison { get; set; }
        public int? FkCommande { get; set; }
        public int? FkTiersLivraison { get; set; }
        public string Nom { get; set; }
        public string Adresse1 { get; set; }
        public string Adresse2 { get; set; }
        public string Adresse3 { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public DateTime? DateLivraison { get; set; }
        public int? Quantite { get; set; }
    }
}
