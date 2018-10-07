using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Amalgame
    {
        public int PkAmalgame { get; set; }
        public int? FkLot { get; set; }
        public int? FkTypeElement { get; set; }
        public int? FkTiers { get; set; }
        public string Nom { get; set; }
    }
}
