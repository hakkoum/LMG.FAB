using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class VwLot
    {
        public int PkLot { get; set; }
        public string NomLot { get; set; }
        public int FkParamDetailProc { get; set; }
        public string CodeLot { get; set; }

        public bool? EnCours { get; set; }
        public int? FkOffices { get; set; }
        public DateTime? DateArriveeLivres { get; set; }
        public DateTime? DateMiseEnVente { get; set; }
        public string NomProcessus { get; set; }
        public string CodeProcessus { get; set; }
    }
}
