using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class FichePapier
    {
        public int PkFichePapier { get; set; }
        public int? NumFiche { get; set; }
        public string Denomination { get; set; }
        public decimal? LargeurMm { get; set; }
        public decimal? Hauteur { get; set; }
        public int? Grammage { get; set; }
    }
}
