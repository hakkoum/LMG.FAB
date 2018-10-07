using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Commande
    {
        public int PkCommande { get; set; }
        public int? FkTiersFournisseur { get; set; }
        public string NumCommandeErp { get; set; }
        public int FkDevis { get; set; }
    }
}
