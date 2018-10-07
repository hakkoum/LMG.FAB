using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.Reflmg
{
    public partial class ProfileFonction
    {
        public int PkProfileFonction { get; set; }
        public int? PosProfile { get; set; }
        public int? FkProfile { get; set; }
        public int? FkFonction { get; set; }

        public Fonction FkFonctionNavigation { get; set; }
        public Profile FkProfileNavigation { get; set; }
    }
}
