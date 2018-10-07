using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class ElImpositionFeuille
    {
        public int PkElement { get; set; }
        public bool? Imposition64Pages { get; set; }
        public int? NbPagesParFeuille1 { get; set; }
        public int? NbPagesParFeuille2 { get; set; }
        public int? NbPagesParFeuille3 { get; set; }
        public int? NbPagesChute { get; set; }
        public decimal? NbFeuilles1 { get; set; }
        public decimal? NbFeuilles2 { get; set; }
        public decimal? NbFeuilles3 { get; set; }
    }
}
