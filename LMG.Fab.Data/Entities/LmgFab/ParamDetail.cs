using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class ParamDetail
    {
        public ParamDetail()
        {
            Lot = new HashSet<Lot>();
            MachineGamme = new HashSet<MachineGamme>();
            Prestation = new HashSet<Prestation>();
            Tiers = new HashSet<Tiers>();
        }

        public int PkParamDetail { get; set; }
        public int FkParamTable { get; set; }
        public string Code { get; set; }
        public string LibelleLong { get; set; }
        public string LibelleCourt { get; set; }
        public int? Tri { get; set; }
        public bool? Actif { get; set; }

        public ParamTable FkParamTableNavigation { get; set; }
        public ICollection<Lot> Lot { get; set; }
        public ICollection<MachineGamme> MachineGamme { get; set; }
        public ICollection<Prestation> Prestation { get; set; }
        public ICollection<Tiers> Tiers { get; set; }
    }
}
