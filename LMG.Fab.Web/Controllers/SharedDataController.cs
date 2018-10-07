using LMG.Fab.Data.Entities.LmgFab;
using LMG.Fab.Web.Models;
using LMG.FAB.Services.Business;
using LMG.FAB.Services.Entities;
using LMG.FAB.Services.FAB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMG.FAB.Util;
using System.Net.Http;
using System.Net;

namespace LMG.Fab.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/shared")]
    //[Authorize]
    public class SharedDataController : Controller
    {
        private ISharedDataManager _sharedDataManager;
        private IAuditManager _auditManager;
        private ILockManager _lockManager;
        private ITiersManager _tiersManager;
        public SharedDataController(ISharedDataManager sharedDataManager, IAuditManager auditManager,
            ILockManager lockManager, ITiersManager tiersManager)
        {
            _sharedDataManager = sharedDataManager;
            _auditManager = auditManager;
            _lockManager = lockManager;
            _tiersManager = tiersManager;
        }

        [HttpGet]
        [Route("process-list")]
        public List<ParamDetail> GetProcessusList()
        {
            return _sharedDataManager.GetParamDetail("PROC");
        }

        [HttpGet]
        [Route("param-values")]
        public List<ParamDetail> GetParamValues(string paramCode)
        {
            return _sharedDataManager.GetParamDetail(paramCode).OrderBy(p=>p.LibelleCourt).ToList();
        }

        [HttpGet]
        [Route("param-list")]
        public List<ParamTable> GetParamList()
        {
            return _sharedDataManager.GetParamTable();
        }

        [HttpGet]
        [Route("coll-list")]
        public List<CollectionTechnique> GetCollectionTechnique()
        {
            return _sharedDataManager.GetCollectionTechnique();
        }

        [HttpPost]
        [Route("param-save")]
        public ParamTable SaveParamTable([FromBody]ParamTable param)
        {
            var result = _sharedDataManager.SaveParamTable(param);
            return result;
        }


        [HttpPost]
        [Route("param-detail-save")]
        public ParamDetail SaveParamDetail([FromBody]ParamDetail param)
        {
            var result = _sharedDataManager.SaveParamDetail(param);
            return result;
        }

        [HttpGet]
        [Route("param-delete")]
        public JsonResult DeleteParamTable(int paramId)
        {
            _sharedDataManager.DeleteParamTable(paramId);
            return Json(true);
        }


        [HttpGet]
        [Route("param-detail-delete")]
        public JsonResult DeleteParamDetail(int paramDetailId)
        {
            _sharedDataManager.DeleteParamDetail(paramDetailId);
            return Json(true);
        }



        [HttpGet]
        [Route("audit-history")]
        public List<Audit> GetAuditHistory(string tableName = null, string operationType = null, int? recordKey = null, DateTime? operationDateDebut = null, DateTime? operationDateFin = null, string fieldFilter = null, string userFilter = null)
        {
            PagedData<Audit> result = new PagedData<Audit>();
            var tmp = _auditManager.GetAuditHistory(tableName, operationDateDebut, operationDateFin, recordKey, operationType, fieldFilter, userFilter);

            return tmp.ToList();
        }

        [HttpGet]
        [Route("db-fields-list")]
        public List<DbField> GetDbFields()
        {
            var tmp = _sharedDataManager.GetDbFields();
            List<DbField> result = new List<DbField>();
            foreach (var item in tmp)
            {
                result.Add(new DbField()
                {
                    TableName = item.Key,
                    Fields = item.Value
                });
            }
            return result.OrderBy(p => p.TableName).ToList();
        }

        [HttpGet]
        [Route("lock-delete")]
        public JsonResult DeleteLock(string tableName, int recordKey)
        {
            _lockManager.DeleteTableLock(tableName, recordKey);
            return Json(true);
        }

        [HttpGet]
        [Route("lock-add")]
        public JsonResult AddLock(string tableName, int recordKey)
        {
            _lockManager.SetTableLock(tableName, recordKey);
            return Json(true);
        }

        [HttpGet]
        [Route("lock-get")]
        public TableLock GetLock(string tableName, int recordKey)
        {
            return _lockManager.GetTableLock(tableName, recordKey);
        }

        [HttpGet]
        [Route("comment-list")]
        public List<Commentaire> GetComments(string tableName, int recordKey)
        {
            return _sharedDataManager.GetRecordComments(tableName, recordKey);
        }

        [HttpGet]
        [Route("comment-byId")]
        public Commentaire GetComment(int commentId)
        {
            return _sharedDataManager.GetComment(commentId);
        }

        [HttpGet]
        [Route("comment-add")]
        public Commentaire AddComment(string tableName, int recordKey, string fieldName, string comment, int isImportant)
        {
            return _sharedDataManager.AddComment(tableName, recordKey, fieldName, comment, isImportant);
        }

        [HttpGet]
        [Route("comment-update")]
        public void UpdateComment(int commentId, string comment, int isImportant)
        {
            _sharedDataManager.UpdateComment(commentId, comment, isImportant);
        }

        [HttpGet]
        [Route("comment-delete")]
        public JsonResult DeleteComment(int commentId)
        {
            _sharedDataManager.DeleteComment(commentId);
            return Json(true);
        }

        [HttpGet]
        [Route("tiers-list")]
        public List<Tiers> GetTiers()
        {
            return _tiersManager.GetTiersList();
        }

        [HttpGet]
        [Route("tiers-info")]
        public Tiers GetTiersInfo(int tiersId)
        {
            return _tiersManager.GetTiers(tiersId);
        }
    }
}
