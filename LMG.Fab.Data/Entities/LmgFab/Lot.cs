using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Lot
    {
        public int PkLot { get; set; }
        public string NomLot { get; set; }
        public int? FkParamDetailProc { get; set; }
        public string CodeLot { get; set; }
        public int? FkOffices { get; set; }
        public DateTime? DateArriveeLivres { get; set; }
        public DateTime? DateMiseEnVente { get; set; }
        public bool? EnCours { get; set; }

        public ParamDetail FkParamDetailProcNavigation { get; set; }
    }
}
