using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class ElementFichePapier
    {
        public int PkElementFichePapier { get; set; }
        public int? FkElement { get; set; }
        public int? FkFichePapier { get; set; }
    }
}
