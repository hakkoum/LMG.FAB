using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class AuditDetail
    {
        public int AuditDetailId { get; set; }
        public int AuditId { get; set; }
        public string Field { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        public Audit Audit { get; set; }
    }
}
