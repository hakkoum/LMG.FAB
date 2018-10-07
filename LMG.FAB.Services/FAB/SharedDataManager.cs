using LMG.Fab.Data.Entities.LmgFab;
using LMG.Fab.Data.Entities.Reflmg;
using LMG.FAB.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMG.FAB.Services.FAB
{
    public class SharedDataManager : ISharedDataManager
    {
        private LMG_FABContext _fabContext;
        private IUserManager _userManager;
        private IUserService _userService;
        public SharedDataManager(LMG_FABContext context, IUserManager userManager, IUserService userService)
        {
            _fabContext = context;
            _userManager = userManager;
            _userService = userService;
        }

        private static Dictionary<string, List<string>> DbFields = null;
        private static Dictionary<string, string> usersInfo = null;
        private static readonly object DbFieldsLock = new object();
        private static readonly object usersInfoLock = new object();

        public Dictionary<string, List<string>> GetDbFields()
        {
            lock (DbFieldsLock)
            {
                if (DbFields == null)
                {
                    DbFields = new Dictionary<string, List<string>>();
                    var tables = _fabContext.Model.GetEntityTypes();
                    foreach (var table in tables)
                    {
                        string tableName = table.Name?.Split(".")?.LastOrDefault()?.ToLower();
                        if (tableName.StartsWith("vw") || tableName == "audit" || tableName == "auditdetail")
                        {
                            continue;
                        }
                        var fields = table.GetProperties();
                        var tmp = new List<string>();
                        foreach (var field in fields)
                        {
                            if (((Property)field).PrimaryKey == null)
                            {
                                tmp.Add(field.Name);
                            }
                        }
                        DbFields.Add(tableName, tmp);
                    }
                }
            }

            return DbFields;
        }

        public string GetUserName()
        {
            string result = "";
            string userLogin = _userService.GetCurrentUser();
            if (string.IsNullOrWhiteSpace(userLogin))
            {
                return "";
            }
            lock (DbFieldsLock)
            {
                if (usersInfo == null)
                {
                    usersInfo = new Dictionary<string, string>();
                }
                if (!usersInfo.ContainsKey(userLogin))
                {
                    var contr = _userManager.GetUser();
                    usersInfo.Add(userLogin, contr.Nom + " " + contr.Prenom);
                }
                result = usersInfo[userLogin];
            }
            return result;
        }

        public Commentaire AddComment(string tableName, int recordKey, string fieldName, string comment, int isImportant)
        {
            Commentaire commentaire = new Commentaire();
            commentaire.TableComm = tableName;
            commentaire.RecordKey = recordKey;
            commentaire.LastUpdate = DateTime.Now;
            commentaire.FieldComm = fieldName;
            commentaire.IsImportant = (isImportant == 1);
            commentaire.CommentaireHtml = comment;
            commentaire.UserName = GetUserName();

            var tableEntityType = _fabContext.Model.GetEntityTypes().FirstOrDefault(p => p.Name?.Split(".")?.LastOrDefault()?.ToLower() == tableName.ToLower());
            if (tableEntityType != null)
            {
                var relatedObject = _fabContext.Find(tableEntityType.ClrType, recordKey);
                commentaire.RecordName = relatedObject?.ToString();
            }

            _fabContext.Commentaire.Add(commentaire);
            _fabContext.SaveChanges();
            return commentaire;
        }

        public void UpdateComment(int commentId, string comment, int isImportant)
        {
            Commentaire commentaire = GetComment(commentId);
            commentaire.CommentaireHtml = comment;
            commentaire.IsImportant = (isImportant == 1);
            commentaire.LastUpdate = DateTime.Now;
            commentaire.UserName = GetUserName();
            _fabContext.SaveChanges();
        }

        public void DeleteComment(int commentId)
        {
            Commentaire commentaire = GetComment(commentId);
            _fabContext.Commentaire.Remove(commentaire);
            _fabContext.SaveChanges();
        }

        public List<Commentaire> GetRecordComments(string tableName, int recordKey)
        {
            return _fabContext.Commentaire.Where(p => p.TableComm.ToLower() == tableName && p.RecordKey == recordKey).ToList();
        }

        public Commentaire GetComment(int commentId)
        {
            Commentaire commentaire = _fabContext.Commentaire.Find(commentId);
            if (commentaire == null)
            {
                throw new Exception($"Le commentaire [pk_commentaire = {commentId}] n'existe pas");
            }
            return commentaire;
        }

        public List<ParamDetail> GetParamDetail(string codeParam)
        {
            return _fabContext.ParamDetail.Where(p => p.FkParamTableNavigation.Code == codeParam).ToList();
        }

        public List<ParamDetail> GetParamDetail()
        {
            return _fabContext.ParamDetail.ToList();
        }

        public List<ParamTable> GetParamTable()
        {
            return _fabContext.ParamTable.Include(p => p.ParamDetail).ToList();
        }

        public ParamTable SaveParamTable(ParamTable param)
        {
            List<int> idsTodelete = new List<int>(); ;
            if (param.PkParamTable > 0)
            {
                var initialIds = param.ParamDetail.Select(p => p.PkParamDetail).ToList();
                var dbIds = _fabContext.ParamDetail.Where(p => p.FkParamTable == param.PkParamTable).Select(p => p.PkParamDetail).ToList();
                idsTodelete = dbIds.Except(initialIds).ToList();
                _fabContext.ParamTable.Update(param);
                _fabContext.SaveChanges();
            }
            else
            {
                _fabContext.ParamTable.Add(param);
                _fabContext.SaveChanges();
            }

            var result = _fabContext.ParamTable.Include(p => p.ParamDetail).First(p => p.PkParamTable == param.PkParamTable);
            //delete param details
            if (idsTodelete.Count() > 0)
            {
                var toDelete = _fabContext.ParamDetail.Where(p => idsTodelete.Contains(p.PkParamDetail));
                _fabContext.ParamDetail.RemoveRange(toDelete);
                _fabContext.SaveChanges();
            }
            return result;
        }

        public ParamDetail SaveParamDetail(ParamDetail detail)
        {
            if (detail.PkParamDetail > 0)
            {
                _fabContext.ParamDetail.Update(detail);
                _fabContext.SaveChanges();
            }
            else
            {
                _fabContext.ParamDetail.Add(detail);
                _fabContext.SaveChanges();
            }
            return detail;
        }

        public void DeleteParamTable(int paramId)
        {
            ParamTable param = _fabContext.Find(typeof(ParamTable), paramId) as ParamTable;
            if (param != null)
            {
                var details = _fabContext.ParamDetail.Where(p => p.FkParamTable == param.PkParamTable);
                if (details.Count() > 0)
                {
                    _fabContext.ParamDetail.RemoveRange(details);

                }
                _fabContext.ParamTable.Remove(param);
                _fabContext.SaveChanges();

            }

        }

        public void DeleteParamDetail(int paramDetailId)
        {
            ParamDetail param = _fabContext.Find(typeof(ParamDetail), paramDetailId) as ParamDetail;
            if (param != null)
            {
                _fabContext.ParamDetail.Remove(param);
                _fabContext.SaveChanges();
            }

        }

        public List<CollectionTechnique> GetCollectionTechnique()
        {
            return _fabContext.CollectionTechnique.ToList();
        }
    }
}
