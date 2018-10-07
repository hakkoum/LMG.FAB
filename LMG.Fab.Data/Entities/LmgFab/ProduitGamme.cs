using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class ProduitGamme
    {
        public int PkProduitGamme { get; set; }
        public int? FkProduit { get; set; }
        public int? FkParamDetailGamm { get; set; }
    }
}
