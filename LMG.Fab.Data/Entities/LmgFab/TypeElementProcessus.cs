using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class TypeElementProcessus
    {
        public int PkTypeElementProcessus { get; set; }
        public int? FkTypeElement { get; set; }
        public int? FkParamDetailProc { get; set; }
    }
}
