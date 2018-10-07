using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using LMG.Fab.Data.Entities.Reflmg;
using LMG.FAB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LMG.Fab.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserManager _usermanager;
        public IConfiguration _config;

        public HomeController(IUserManager usermanager, IConfiguration configuration)
        {
            _usermanager = usermanager;
            _config = configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            Contributeur user = _usermanager.GetUser();
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The user {User?.Identity?.Name} is not regonised, please contact your administrator";
                return View("Error");
            }
            else
            {
                string appCode = _config.GetValue<string>("appCode");
                if (!_usermanager.IsUserAllowedToApp(user.PkContributeur, appCode))
                {
                    ViewBag.ErrorMessage = $"The user {User?.Identity?.Name} is not allowed to use the application {appCode}, please contact your administrator";
                    return View("Error");
                }
            }
            ViewBag.Version = _config.GetValue<string>("version");
            ViewBag.UserName = user.Prenom + user.Nom; 
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
