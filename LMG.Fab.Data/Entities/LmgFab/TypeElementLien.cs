using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class TypeElementLien
    {
        public int PkTypeElementLien { get; set; }
        public int? FkTypeElementParent { get; set; }
        public int? FkTypeElementEnfant { get; set; }
        public bool? OkHeritageFournisseur { get; set; }
    }
}
