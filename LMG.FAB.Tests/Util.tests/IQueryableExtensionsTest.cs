using LMG.Fab.Data.Entities.Reflmg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMG.FAB.Util;

namespace LMG.FAB.Tests.Util.tests
{
    [TestClass]
    public class IQueryableExtensionsTest
    {
        private static ServiceProvider serviceProvider;

        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddDbContext<ReflmgContext>(op => op.UseInMemoryDatabase("ReflmgDB_test"), ServiceLifetime.Transient);
            serviceProvider = services.BuildServiceProvider();

        }

        [TestMethod]
        public void OrderByPropNameTest()
        {
            var dbContext = serviceProvider.GetService<ReflmgContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Contributeur.Add(new Contributeur() { PkContributeur = 1, Login = "11" });
            dbContext.Contributeur.Add(new Contributeur() { PkContributeur = 2, Login = "00" });
            dbContext.Contributeur.Add(new Contributeur() { PkContributeur = 3, Login = "22" });
            dbContext.SaveChanges();

            //trier la liste par le nom d'une propriété
            var result = dbContext.Contributeur.AsQueryable().OrderBy("Login").ToList() ;

            Assert.AreEqual(2, result[0].PkContributeur);
            Assert.AreEqual(1, result[1].PkContributeur);
            Assert.AreEqual(3, result[2].PkContributeur);

            result = dbContext.Contributeur.AsQueryable().OrderByDescending("Login").ToList();

            Assert.AreEqual(3, result[0].PkContributeur);
            Assert.AreEqual(1, result[1].PkContributeur);
            Assert.AreEqual(2, result[2].PkContributeur);

        }
    }
}
