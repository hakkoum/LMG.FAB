using LMG.FAB.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Services.Business
{
    public interface ILockManager
    {
        TableLock GetTableLock(string tableName, int recordId);

        void SetTableLock(string tableName, int recordId);

        void DeleteTableLock(string tableName, int recordId);
    }
}
