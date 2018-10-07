using LMG.Fab.Data.Entities.Reflmg;
using LMG.FAB.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMG.Fab.Data.Entities.LmgFab
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public string RecordName { get; set; }

        public string OperationType { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit(IUserService userService, ReflmgContext refContext)
        {
            var audit = new Audit();
            audit.TableName = TableName;
            audit.OperationType = OperationType;
            audit.OperationDate = DateTime.UtcNow;
            var userLogin = userService?.GetCurrentDbUser();
            Contributeur user = refContext.Contributeur.FirstOrDefault(p => p.Login == userLogin);
            if (user == null)
            {
                audit.UserName = userLogin;
            }
            else
            {
                audit.UserName = user.Prenom + " " + user.Nom;
            }
            audit.RecordKey = int.Parse(KeyValues.First().Value.ToString());
            audit.RecordName = RecordName;
            List<AuditEntryDetail> detail = new List<AuditEntryDetail>();
            OldValues.Keys.Union(NewValues.Keys).Distinct().ToList()
                    .ForEach(key => detail.Add(new AuditEntryDetail() { Field = key }));
            foreach (var item in detail)
            {
                item.NewValue = NewValues.ContainsKey(item.Field) ? NewValues[item.Field] : null;
                item.OldValue = OldValues.ContainsKey(item.Field) ? OldValues[item.Field] : null;
                AuditDetail auditDetail = new AuditDetail();
                auditDetail.Field = item.Field;
                auditDetail.NewValue = item.NewValue?.ToString();
                auditDetail.OldValue = item.OldValue?.ToString();
                audit.AuditDetail.Add(auditDetail);
            }
            return audit;
        }
    }

    public class AuditEntryDetail
    {
        public string Field { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
