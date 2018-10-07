using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Devis
    {
        public bool? FichierFourni { get; set; }
        public int PkDevis { get; set; }
        public int FkComposant { get; set; }
        public bool? OkActif { get; set; }
        public int? FkParamDetailStfb { get; set; }
        public int? NbHeuresSuiviFab { get; set; }
        public int? FkParamDetailSadm { get; set; }
        public int? FkCollectionTechnique { get; set; }
        public int? FkParamDetailPres { get; set; }
    }
}
