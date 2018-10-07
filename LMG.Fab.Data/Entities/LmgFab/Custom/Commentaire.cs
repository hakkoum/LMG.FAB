using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Commentaire
    {
        public override string ToString()
        {
            return $"{TableComm}-{RecordName}-{FieldComm}";
        }
    }
}
