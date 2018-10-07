using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class FicheStock
    {
        public int PkFicheStock { get; set; }
        public int? NumFiche { get; set; }
        public string Denomination { get; set; }
        public decimal? LargeurCm { get; set; }
        public decimal? HauteurCm { get; set; }
        public decimal? Grammage { get; set; }
    }
}
