using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Produit
    {
        public int PkProduit { get; set; }
        public string Famille { get; set; }
        public string Intitule { get; set; }
        public decimal? Grammage { get; set; }
        public decimal? PrixKg { get; set; }
        public decimal? Main { get; set; }
        public decimal? LargeurMm { get; set; }
        public decimal? HauteurMm { get; set; }
        public bool? OkRoto { get; set; }
        public bool? OkFeuille { get; set; }
        public bool? DefiniParUtil { get; set; }
        public int? FkParamDetailQual { get; set; }
    }
}
