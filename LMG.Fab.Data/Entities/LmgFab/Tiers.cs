using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Tiers
    {
        public Tiers()
        {
            Prestation = new HashSet<Prestation>();
        }

        public int PkTiers { get; set; }
        public int? FkParamDetailType { get; set; }
        public string Nom { get; set; }
        public string Adresse1 { get; set; }
        public string Adresse2 { get; set; }
        public string Adresse3 { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public string NomContact { get; set; }
        public string Telephone { get; set; }
        public string CodeFournisseurErp { get; set; }
        public string CodeEntrepotErp { get; set; }
        public string SignatureFicheTechnique { get; set; }
        public string TexteCommande { get; set; }
        public bool? Actif { get; set; }
        public string AdresseEmail { get; set; }
        public bool? OkImprimvert { get; set; }

        public ParamDetail FkParamDetailTypeNavigation { get; set; }
        public ICollection<Prestation> Prestation { get; set; }
    }
}
