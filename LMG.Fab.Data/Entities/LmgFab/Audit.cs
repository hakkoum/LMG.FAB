using System;
using System.Collections.Generic;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public partial class Audit
    {
        public Audit()
        {
            AuditDetail = new HashSet<AuditDetail>();
        }

        public int AuditId { get; set; }
        public string UserName { get; set; }
        public DateTime OperationDate { get; set; }
        public string TableName { get; set; }
        public string OperationType { get; set; }
        public string RecordName { get; set; }
        public int RecordKey { get; set; }

        public ICollection<AuditDetail> AuditDetail { get; set; }
    }
}
