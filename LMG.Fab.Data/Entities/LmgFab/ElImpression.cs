using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class ElImpression
    {
        public int PkElImpression { get; set; }
        public int? FkMachine { get; set; }
        public int? FkParamDetailPell { get; set; }
        public int? FkParamDetailVern { get; set; }
        public int? FkParamDetailCoulRecto { get; set; }
        public int? FkParamDetailCoulVerso { get; set; }
        public int? FkParamdetailType { get; set; }
        public bool? OkSupplementSimili { get; set; }
        public bool? OkCoteAcote { get; set; }
        public int? FkParamDetailFilm { get; set; }
        public decimal? CoutFilm { get; set; }
        public bool? OkEquerrage { get; set; }
        public decimal? EquerrageFraisFixe { get; set; }
        public decimal? EquerrageFraisVariable { get; set; }
    }
}
