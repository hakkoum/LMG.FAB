using LMG.Fab.Data.Entities.Reflmg;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LMG.FAB.Services
{
    public interface IUserManager
    {
        /// <summary>
        /// retourne le Contributeur depuis le login windows de l'utilisateur connecté
        /// </summary>
        /// <returns></returns>
        Contributeur GetUser();

        /// <summary>
        /// retourne vrai si l'utilisateur a droit à l'application, et faux sinon
        /// </summary>
        /// <param name="userId">l'id de la table Contributeur</param>
        /// <param name="appName"></param>
        /// <returns></returns>
        bool IsUserAllowedToApp(int userId, string appName);

        /// <summary>
        /// retourne la liste des fonctions auxquelles l'utilisateur a droit 
        /// </summary>
        /// <param name="userLogin">Le login windows de l'utilisateur</param>
        /// <param name="appName">Le code de l'application</param>
        /// <returns></returns>
        List<string> GetUserRights(string userLogin, string appName);


        /// <summary>
        /// retourne la liste des fonctions auxquelles l'utilisateur a droit 
        /// </summary>
        /// <param name="userId">l'id de la table Contributeu</param>
        /// <param name="appName">Le code de l'applicatio</param>
        /// <returns></returns>
        List<string> GetUserRights(int userId, string appName);

    }
}
