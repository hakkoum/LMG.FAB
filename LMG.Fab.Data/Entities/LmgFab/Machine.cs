using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Machine
    {
        public Machine()
        {
            MachineGamme = new HashSet<MachineGamme>();
        }

        public int PkMachine { get; set; }
        public string Nom { get; set; }
        public string Tournees { get; set; }
        public bool? Roto { get; set; }
        public bool? Coming { get; set; }
        public bool? Cameron { get; set; }
        public decimal? FausseCoupe { get; set; }
        public int? FkPrestation { get; set; }
        public string SousMultiplesCahiers { get; set; }
        public bool? OkNumerique { get; set; }

        public Prestation FkPrestationNavigation { get; set; }
        public ICollection<MachineGamme> MachineGamme { get; set; }
    }
}
