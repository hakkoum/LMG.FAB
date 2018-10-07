using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Composant
    {
        public int PkComposant { get; set; }
        public int FkDossier { get; set; }
        public string Nom { get; set; }
    }
}
