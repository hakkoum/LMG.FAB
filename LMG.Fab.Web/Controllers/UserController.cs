using LMG.Fab.Data.Entities.Reflmg;
using LMG.Fab.Web.Models;
using LMG.FAB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMG.Fab.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    [Authorize]
    public class UserController : Controller
    {
        private IUserManager _usermanager;
        public IConfiguration _config;

        public UserController(IUserManager usermanager, IConfiguration configuration)
        {
            _usermanager = usermanager;
            _config = configuration;
        }

        /// <summary>
        /// recupere la liste des operations auxquelles l'utilisateur à droit
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("userProfil")]
        //[Authorize(Policy = "FAB_ADMIN")]
        public UserProfil GetUserRights()
        {
            UserProfil profil = new UserProfil();
            Contributeur user = _usermanager.GetUser();
            if (user == null)
            {
                return profil;
            }
            profil.UserId = user.PkContributeur;
            profil.FirstName = user.Prenom;
            profil.LastName = user.Nom;
            string appCode = _config.GetValue<string>("appCode");
            profil.RightsList = _usermanager.GetUserRights(user.PkContributeur, appCode);
            return profil;
        }
    }
}
