using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class Perimetre
    {
        public string Nom { get; set; }
        public string Objet { get; set; }
        public string Filtre { get; set; }
        public int PkPerimetre { get; set; }
        public string CortexCulture { get; set; }
        public string ClauseWhere { get; set; }
        public string LibelleSso { get; set; }
        public string Esql { get; set; }

        public NomPerimetre NomNavigation { get; set; }
    }
}
