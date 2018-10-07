using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class AmalgameDetail
    {
        public int PkAmalgameDetail { get; set; }
        public int? FkAmalgame { get; set; }
        public int? FkElement { get; set; }
    }
}
