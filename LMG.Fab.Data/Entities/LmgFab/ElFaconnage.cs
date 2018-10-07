using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class ElFaconnage
    {
        public int PkElement { get; set; }
        public bool? FaconnerTout { get; set; }
        public int? FkParamDetailTbro { get; set; }
        public int? FkProduitToile { get; set; }
        public int? FkParamDetailGard { get; set; }
        public int? Quantite { get; set; }
        public decimal? ValeurToile { get; set; }
        public decimal? FerAdorer { get; set; }
        public decimal? FraisFixePose { get; set; }
        public decimal? FraisVariablePose { get; set; }
    }
}
