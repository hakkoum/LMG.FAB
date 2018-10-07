using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Dossier
    {
        public int PkDossier { get; set; }
        public string IdTirage { get; set; }
        public int? FkLot { get; set; }
        public decimal? PrixTtc { get; set; }
        public bool? OkCoffret { get; set; }
        public string FilierePrincipale { get; set; }
        public int? FkParamDetailProc { get; set; }
        public int? IdTirage4d { get; set; }
        public int? QteTirage { get; set; }
        public bool? OkQteTirageDef { get; set; }
        public bool? OkLivraisonAnticipee { get; set; }
        public bool? FausseFoliotation { get; set; }
        public string TxtModeleComposition { get; set; }
        public int? FkParamDetailEqfb { get; set; }
        public bool? OkEligibleTechniqueEpac { get; set; }
    }
}
