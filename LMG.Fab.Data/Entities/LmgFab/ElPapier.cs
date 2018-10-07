using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class ElPapier
    {
        public int PkElement { get; set; }
        public int? FkProduit { get; set; }
        public int? FkParamDetailQual { get; set; }
        public decimal? Grammage { get; set; }
        public decimal? Prix { get; set; }
        public decimal? Main { get; set; }
        public decimal? LargeurMm { get; set; }
        public decimal? HauteurMm { get; set; }
        public bool? PapierEco { get; set; }
    }
}
