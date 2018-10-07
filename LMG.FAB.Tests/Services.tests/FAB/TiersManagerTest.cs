using LMG.Fab.Data.Entities.LmgFab;
using LMG.Fab.Data.Entities.Reflmg;
using LMG.FAB.Services;
using LMG.FAB.Services.FAB;
using LMG.FAB.Tests.Services.tests.Mocks;
using LMG.FAB.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Tests.Services.tests.FAB
{
    [TestClass]
    public class TiersManagerTest
    {
        private static ServiceProvider serviceProvider;
        private ITiersManager tiersManager;



        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddTransient<ITiersManager, TiersManager>();
            services.AddTransient<IUserService, TestUserService>();
            services.AddDbContext<LMG_FABContext>(op => op.UseInMemoryDatabase("LmgFabDB_test"), ServiceLifetime.Transient);

            services.AddDbContext<ReflmgContext>(op => op.UseInMemoryDatabase("ReflmgDB_test"), ServiceLifetime.Transient);

            //var mockContextAccessor = new Mock<IHttpContextAccessor>();
            //mockContextAccessor.Setup(p => p.HttpContext.User.Identity.Name).Returns("TestUser");

            serviceProvider = services.BuildServiceProvider();

        }

        [TestMethod]
        public void GetTiersListTest()
        {
            var dbContext = serviceProvider.GetService<LMG_FABContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Tiers.Add(new Tiers() { PkTiers = 1, Actif = true, FkParamDetailType = null });
            dbContext.Tiers.Add(new Tiers() { PkTiers = 2, Actif = false, FkParamDetailType = null });
            dbContext.Prestation.Add(new Prestation() { PkPrestation = 1, FkTiers = 1, FkParamDetailTrfo = null });
            dbContext.Prestation.Add(new Prestation() { PkPrestation = 2, FkTiers = 1, FkParamDetailTrfo = null });
            dbContext.Prestation.Add(new Prestation() { PkPrestation = 3, FkTiers = 2, FkParamDetailTrfo = null });
            dbContext.SaveChanges();

            tiersManager = serviceProvider.GetService<ITiersManager>();

            var result = tiersManager.GetTiersList();
            Assert.AreEqual(2, result.Count);

        }

        [TestMethod]
        public void GetTiersTest()
        {
            var dbContext = serviceProvider.GetService<LMG_FABContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Tiers.Add(new Tiers() { PkTiers = 1, Actif = true, FkParamDetailType = null});
            dbContext.Tiers.Add(new Tiers() { PkTiers = 2, Actif = false , FkParamDetailType = null });
            dbContext.Prestation.Add(new Prestation() { PkPrestation= 1, FkTiers = 1, FkParamDetailTrfo = null });
            dbContext.Prestation.Add(new Prestation() { PkPrestation = 2, FkTiers = 1 , FkParamDetailTrfo = null });
            dbContext.Prestation.Add(new Prestation() { PkPrestation = 3, FkTiers = 2, FkParamDetailTrfo = null });
            dbContext.SaveChanges();

            tiersManager = serviceProvider.GetService<ITiersManager>();

            var result = tiersManager.GetTiers(1);
            Assert.AreEqual(2, result.Prestation.Count);

        }
    }
}
