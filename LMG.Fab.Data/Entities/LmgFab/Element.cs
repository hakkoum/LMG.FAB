using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Element
    {
        public int PkElement { get; set; }
        public int? FkDevis { get; set; }
        public int? FkTypeElement { get; set; }
        public decimal? FraisFixe { get; set; }
        public decimal? FraisVariable { get; set; }
        public decimal? PortFixe { get; set; }
        public int? FkTiersFournisseur { get; set; }
        public decimal? MontantCalculDevis { get; set; }
        public decimal? MontantCalculCommande { get; set; }
        public int? FkElementParent { get; set; }
        public bool? OkCommande { get; set; }
    }
}
