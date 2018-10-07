using LMG.Fab.Data.Entities.Reflmg;
using LMG.FAB.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMG.FAB.Services
{
    public class UserManager : IUserManager
    {
        private ReflmgContext _refContext;
        private IUserService _userService;
        public UserManager(ReflmgContext context, IUserService userService)
        {
            _refContext = context;
            _userService = userService;
        }

        public Contributeur GetUser()
        {
            string userLogin = _userService.GetCurrentDbUser();
            Contributeur user = _refContext.Contributeur.FirstOrDefault(p => p.Login == userLogin);
            return user;
        }

        public List<string> GetUserRights(int userId, string appName)
        {
            List<string> rights = new List<string>();
            var fonctions = from fc in _refContext.Fonction
                            join profil_fc in _refContext.ProfileFonction on fc.PkFonction equals profil_fc.FkFonction
                            join profil in _refContext.Profile on profil_fc.FkProfile equals profil.PkProfile
                            join role_app in _refContext.RoleParApplication on profil.PkProfile equals role_app.ProfilId
                            join app in _refContext.Application on role_app.ApplicationId equals app.PkApplication
                            where role_app.UtilisateurId == userId && app.Code == appName
                            select fc.Code;
            return fonctions.ToList().Distinct().ToList();
        }

        public List<string> GetUserRights(string userLogin, string appName)
        {
            string userLoginDb = _userService.TransformUser(userLogin);
            Contributeur user = _refContext.Contributeur.FirstOrDefault(p => p.Login == userLoginDb);
            if (user == null)
            {
                return null;
            }
            else
            {
                return GetUserRights(user.PkContributeur, appName);
            }
        }

        public bool IsUserAllowedToApp(int userId, string appName)
        {
            RoleParApplication roleApp = _refContext.RoleParApplication.FirstOrDefault(p => p.UtilisateurId == userId && p.Application.Code == appName);
            if (roleApp == null)
            {
                return false;
            }
            return true;
        }
    }
}
