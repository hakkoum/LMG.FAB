using LMG.Fab.Data.Entities.Reflmg;
using LMG.FAB.Services;
using LMG.FAB.Tests.Services.tests.Mocks;
using LMG.FAB.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Tests.Services.tests.Referentiel
{
    [TestClass]
    public class UserManagerTest
    {

        private static ServiceProvider serviceProvider;
        private IUserManager userManager;

        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IUserService, TestUserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<ReflmgContext>(op => op.UseInMemoryDatabase("ReflmgDB_test"), ServiceLifetime.Transient);

            //var mockContextAccessor = new Mock<IHttpContextAccessor>();
            //mockContextAccessor.Setup(p => p.HttpContext.User.Identity.Name).Returns("TestUser");

            serviceProvider = services.BuildServiceProvider();

        }

        [TestMethod]
        public void GetUserTest()
        {
            var dbContext = serviceProvider.GetService<ReflmgContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Contributeur.Add(new Contributeur() { PkContributeur = 1, Login = "TestUser" });
            dbContext.SaveChanges();

            userManager = serviceProvider.GetService<IUserManager>();

            var user = userManager.GetUser();
            Assert.AreEqual(1, user.PkContributeur);

        }

        [TestMethod]
        public void GetUserRightsTest()
        {
            AddUserFonctionApp();

            userManager = serviceProvider.GetService<IUserManager>();

            var result = userManager.GetUserRights("TestUser", "FAB");

            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.Contains("Admin_Page"));
            Assert.IsTrue(result.Contains("Lot_View"));
            Assert.IsTrue(result.Contains("Lot_Edit"));

            result = userManager.GetUserRights(1, "FAB");
            Assert.IsTrue(result.Count == 3);
            Assert.IsTrue(result.Contains("Admin_Page"));
            Assert.IsTrue(result.Contains("Lot_View"));
            Assert.IsTrue(result.Contains("Lot_Edit"));

            result = userManager.GetUserRights("TestUser2", "FAB");
            Assert.IsTrue(result.Count == 1);
            Assert.IsFalse(result.Contains("Admin_Page"));
            Assert.IsTrue(result.Contains("Lot_View"));

            result = userManager.GetUserRights(2, "FAB");
            Assert.IsTrue(result.Count == 1);
            Assert.IsFalse(result.Contains("Admin_Page"));
            Assert.IsTrue(result.Contains("Lot_View"));

            result = userManager.GetUserRights("TestUser3", "FAB");
            Assert.IsTrue(result.Count == 0);

            result = userManager.GetUserRights(3, "FAB");
            Assert.IsTrue(result.Count == 0);

        }


        [TestMethod]
        public void IsUserAllowedToAppTest()
        {
            AddUserFonctionApp();

            userManager = serviceProvider.GetService<IUserManager>();

            Assert.IsTrue(userManager.IsUserAllowedToApp(1, "FAB"));
            Assert.IsTrue(userManager.IsUserAllowedToApp(2, "FAB"));
            Assert.IsFalse(userManager.IsUserAllowedToApp(3, "FAB"));
        }

        private static void AddUserFonctionApp()
        {
            var dbContext = serviceProvider.GetService<ReflmgContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Contributeur.Add(new Contributeur() { PkContributeur = 1, Login = "TestUser" });
            dbContext.Contributeur.Add(new Contributeur() { PkContributeur = 2, Login = "TestUser2" });
            dbContext.Contributeur.Add(new Contributeur() { PkContributeur = 3, Login = "TestUser3" });
            dbContext.Fonction.Add(new Fonction() { PkFonction = 1, Code = "Admin_Page" });
            dbContext.Fonction.Add(new Fonction() { PkFonction = 2, Code = "Lot_View" });
            dbContext.Fonction.Add(new Fonction() { PkFonction = 3, Code = "Lot_Edit" });
            dbContext.Profile.Add(new Profile() { PkProfile = 1, Nom = "FAB_Admin" });
            dbContext.Profile.Add(new Profile() { PkProfile = 2, Nom = "FAB_Reader" });
            dbContext.ProfileFonction.Add(new ProfileFonction() { FkFonction = 1, FkProfile = 1 });
            dbContext.ProfileFonction.Add(new ProfileFonction() { FkFonction = 2, FkProfile = 1 });
            dbContext.ProfileFonction.Add(new ProfileFonction() { FkFonction = 3, FkProfile = 1 });
            dbContext.ProfileFonction.Add(new ProfileFonction() { FkFonction = 2, FkProfile = 2 });
            dbContext.RoleParApplication.Add(new RoleParApplication() {UtilisateurId = 1, ProfilId = 1, ApplicationId = 1, Perimetre = "Test" });
            dbContext.RoleParApplication.Add(new RoleParApplication() { UtilisateurId = 1, ProfilId = 2, ApplicationId = 1, Perimetre = "Test" });
            dbContext.RoleParApplication.Add(new RoleParApplication() { UtilisateurId = 2, ProfilId = 2, ApplicationId = 1, Perimetre = "Test" });
            dbContext.Application.Add(new Application() { PkApplication = 1, Code = "FAB" });

            dbContext.SaveChanges();
        }


    }
}
