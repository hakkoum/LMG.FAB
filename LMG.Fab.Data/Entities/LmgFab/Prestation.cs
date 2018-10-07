using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Prestation
    {
        public Prestation()
        {
            Machine = new HashSet<Machine>();
        }

        public int PkPrestation { get; set; }
        public int? FkTiers { get; set; }
        public int? FkParamDetailTrfo { get; set; }
        public decimal? TauxHoraire { get; set; }

        public ParamDetail FkParamDetailTrfoNavigation { get; set; }
        public Tiers FkTiersNavigation { get; set; }
        public ICollection<Machine> Machine { get; set; }
    }
}
