using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class MachineGamme
    {
        public int PkMachineGamme { get; set; }
        public int? FkMachine { get; set; }
        public int? FkParamDetailGamm { get; set; }
        public string CodeDatexp { get; set; }
        public bool? Actif { get; set; }

        public Machine FkMachineNavigation { get; set; }
        public ParamDetail FkParamDetailGammNavigation { get; set; }
    }
}
