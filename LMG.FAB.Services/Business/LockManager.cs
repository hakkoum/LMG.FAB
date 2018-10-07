using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMG.FAB.Services.Entities;
using LMG.FAB.Services.FAB;
using LMG.FAB.Util;
using Microsoft.Extensions.Options;

namespace LMG.FAB.Services.Business
{
    public class LockManager : ILockManager
    {
        private static List<TableLock> tableLocks = new List<TableLock>();
        private static readonly object safeLock = new object();
        private readonly AppSettings _settings;

        private ISharedDataManager _sharedDataManager;
        private IUserService _userService;
        public LockManager(ISharedDataManager sharedDataManager, IUserService userService, IOptions<AppSettings> settings)
        {
            _sharedDataManager = sharedDataManager;
            _userService = userService;
            _settings = settings.Value;
        }

        public void DeleteTableLock(string tableName, int recordId)
        {
            string userLogin = _userService.GetCurrentUser();
            lock (safeLock)
            {
                tableLocks.RemoveAll(p => p.TableName.ToLower() == tableName.ToLower() && p.RecordKey == recordId
                                        && p.UserLogin == userLogin
                );
            }
        }

        public TableLock GetTableLock(string tableName, int recordId)
        {
            TableLock result = null;
            string userLogin = _userService.GetCurrentUser();
            lock (safeLock)
            {
                result = tableLocks.FirstOrDefault(p => p.TableName == tableName.ToLower()
                && p.RecordKey == recordId && p.LockDate > DateTime.Now.AddSeconds(-1 * _settings.LockExpireDuration)  
                //&& p.UserLogin != userLogin
                );
            }
            return result;
        }

        public void SetTableLock(string tableName, int recordId)
        {
            string userLogin = _userService.GetCurrentUser();
            lock (safeLock)
            {
                var existingLock = tableLocks.FirstOrDefault(p => p.TableName.ToLower() == tableName.ToLower() && p.RecordKey == recordId);
                if (existingLock == null)
                {
                    TableLock tableLock = new TableLock();
                    tableLock.LockDate = DateTime.Now;
                    tableLock.RecordKey = recordId;
                    tableLock.TableName = tableName?.ToLower();
                    tableLock.UserName = _sharedDataManager.GetUserName();
                    tableLock.UserLogin = _userService.GetCurrentUser();
                    tableLocks.Add(tableLock);
                }
                else
                {
                    existingLock.LockDate = DateTime.Now;
                }
            }
        }
    }
}
