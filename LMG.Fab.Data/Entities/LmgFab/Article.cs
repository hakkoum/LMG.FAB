using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Article
    {
        public int PkArticle { get; set; }
        public int? IdIntranet { get; set; }
        public string FkParamDetailEdit { get; set; }
        public string Titre { get; set; }
        public int? FkParamDetailForm { get; set; }
        public decimal? Hauteur { get; set; }
        public decimal? Largeur { get; set; }
        public int? NbPages { get; set; }
        public int? NbSignesCalibres { get; set; }
        public int? NbSignesEvalues { get; set; }
        public string Observation { get; set; }
        public int? NbPagesHorsTexte { get; set; }
        public bool? NbPagesHorsTexteDef { get; set; }
        public bool? NbPagesInterieurDef { get; set; }
        public int? FkParamDetailTyac { get; set; }
        public int? FkParamDetailGamm { get; set; }
        public int? ParCombien { get; set; }
        public string MotsCles { get; set; }
    }
}
