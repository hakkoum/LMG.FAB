using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class CollectionTechnique
    {
        public int PkCollectionTechnique { get; set; }
        public string Code { get; set; }
        public string Nom { get; set; }
        public int? Tri { get; set; }
        public int? NbJustificatifs { get; set; }
        public string TexteJustificatif { get; set; }
        public int? FkParamDetailGamm { get; set; }
        public bool? OkActif { get; set; }
        public int? FkParamDetailPellCouv { get; set; }
        public int? FkParamDetailVernCouv { get; set; }
        public int? FkParamDetailPellCouvr { get; set; }
        public int? FkParamDetailVernCouvr { get; set; }
        public int? FkParamDetailPellJaquette { get; set; }
        public int? FkParamDetailVernJaquette { get; set; }
    }
}
