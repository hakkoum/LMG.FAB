using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Services.Entities
{
    public class TableLock
    {
        public string TableName { get; set; }
        public int RecordKey { get; set; }
        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public DateTime LockDate { get; set; }
    }
}
