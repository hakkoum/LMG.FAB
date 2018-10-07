using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Util
{
    public interface IUserService
    {
        /// <summary>
        /// retourne le login windows de l'utilisateur connecté 
        /// </summary>
        /// <returns></returns>
        string GetCurrentUser();

        /// <summary>
        /// retourne le login de la base referentiel de l'utilisateur connecté (apres transformation)
        /// </summary>
        /// <returns></returns>
        string GetCurrentDbUser();

        /// <summary>
        /// transforme le login windows au login DB referentiel
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        string TransformUser(string userLogin);
    }
}
