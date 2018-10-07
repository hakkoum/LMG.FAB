using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Commentaire
    {
        public int PkCommentaire { get; set; }
        public string TableComm { get; set; }
        public int RecordKey { get; set; }
        public string RecordName { get; set; }
        public string FieldComm { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UserName { get; set; }
        public string CommentaireHtml { get; set; }
        public bool? IsImportant { get; set; }
    }
}
