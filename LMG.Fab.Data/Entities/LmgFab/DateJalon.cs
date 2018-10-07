using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class DateJalon
    {
        public int PkDateJalon { get; set; }
        public int? FkTypeJalon { get; set; }
        public int? FkDossier { get; set; }
        public DateTime? DateJalon1 { get; set; }
    }
}
