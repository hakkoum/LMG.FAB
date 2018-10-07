using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class TypeElement
    {
        public int PkTypeElement { get; set; }
        public string Code { get; set; }
        public string Nom { get; set; }
        public bool? OkImpression { get; set; }
        public bool? OkPapier { get; set; }
        public bool? OkFaconnage { get; set; }
        public int? Tri { get; set; }
        public bool? OkImpositionFeuille { get; set; }
        public bool? OkImpositionCahier { get; set; }
        public bool? OkMultiple { get; set; }
        public bool? OkPrincipal { get; set; }
        public bool? OkObligatoire { get; set; }
        public bool? OkComposition { get; set; }
    }
}
