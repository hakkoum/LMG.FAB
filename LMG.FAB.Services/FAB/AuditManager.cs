using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMG.Fab.Data.Entities.LmgFab;
using Microsoft.EntityFrameworkCore;

namespace LMG.FAB.Services.FAB
{
    public class AuditManager : IAuditManager
    {
        private LMG_FABContext _fabContext;
        public AuditManager(LMG_FABContext context)
        {
            _fabContext = context;
        }

        public IQueryable<Audit> GetAuditHistory(string tableName, DateTime? operationDateDebut, DateTime? operationDateFin, int? recordKey, string operationType, string fieldFilter, string userFilter)
        {
            if (!operationDateDebut.HasValue)
            {
                operationDateDebut = DateTime.Now.AddMonths(-3);
            }
            if (!operationDateFin.HasValue)
            {
                operationDateFin = DateTime.Now.AddDays(1);
            }

            var query = _fabContext.Audit.AsQueryable();
            if (!string.IsNullOrWhiteSpace(tableName))
            {
                query = query.Where(x => x.TableName == tableName);
            }
            if (recordKey.HasValue && recordKey.Value > 0)
            {
                query = query.Where(x => x.RecordKey == recordKey.Value);
            }
            if (!string.IsNullOrWhiteSpace(operationType))
            {
                if (operationType.Contains(","))
                {
                    var types = operationType.Split(",");
                    query = query.Where(x => types.Contains(x.OperationType));
                }
                else
                {
                    query = query.Where(x => x.OperationType == operationType);
                }
            }
            if (!string.IsNullOrWhiteSpace(userFilter))
            {
                query = query.Where(x => x.UserName.ToLower().Contains(userFilter.ToLower()));
            }

            query = query.Where(x => x.OperationDate >= operationDateDebut.Value);
            query = query.Where(x => x.OperationDate <= operationDateFin.Value);

            if (!string.IsNullOrWhiteSpace(fieldFilter))
            {
                query = query.Where(x => x.AuditDetail.Any(p => p.Field == fieldFilter));
                return query.Select(p => new Audit()
                {
                    AuditId = p.AuditId,
                    OperationDate = p.OperationDate,
                    OperationType = p.OperationType,
                    RecordKey = p.RecordKey,
                    RecordName = p.RecordName,
                    TableName = p.TableName,
                    UserName = p.UserName,
                    AuditDetail = p.AuditDetail.Where(x => x.Field == fieldFilter).ToList(),

                });
            }

            return query.Include(p => p.AuditDetail).AsQueryable();
        }
    }
}
