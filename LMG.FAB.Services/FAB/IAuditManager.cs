using LMG.Fab.Data.Entities.LmgFab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMG.FAB.Services.FAB
{
    public interface IAuditManager
    {
        /// <summary>
        /// Recupere l'historique des modifications pour une table/un enregistrement, par defaut sur les 3 derniers mois
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="operationDateDebut"></param>
        /// <param name="operationDateFin"></param>
        /// <param name="recordKey"></param>
        /// <param name="operationType"></param>
        /// <returns></returns>
        IQueryable<Audit> GetAuditHistory(string tableName, DateTime? operationDateDebut, DateTime? operationDateFin, int? recordKey, string operationType, string fieldFilter, string userFilter);
    }
}
