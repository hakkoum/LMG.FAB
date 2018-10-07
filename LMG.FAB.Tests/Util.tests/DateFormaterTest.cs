using LMG.FAB.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Tests.Util.tests
{
    [TestClass]
    public class DateFormaterTest
    {
        private IDateFormater dataFormater;
        private static ServiceProvider serviceProvider;

        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddTransient<IDateFormater, DateFormater>();
            serviceProvider = services.BuildServiceProvider();
        }

        [TestMethod]
        public void FormatDateToNomLotTest()
        {
            dataFormater = serviceProvider.GetService<IDateFormater>();
            Assert.AreEqual("Janvier 01 2018", dataFormater.FormatDateToNomLot(new DateTime(2018, 1, 1)));
            Assert.AreEqual("Février 10 2018", dataFormater.FormatDateToNomLot(new DateTime(2018, 2, 10)));
            Assert.AreEqual("Mars 28 2022", dataFormater.FormatDateToNomLot(new DateTime(2022, 3, 28)));
        }

        [TestMethod]
        public void FormateDateToCodeLotTest()
        {
            dataFormater = serviceProvider.GetService<IDateFormater>();
            string[] months = { "JV", "FV", "MR", "AV", "MI", "JN", "JL", "AO", "SP", "OC", "NV", "DC" };
            Assert.AreEqual("JV/18/1", dataFormater.FormateDateToCodeLot(new DateTime(2018, 1, 1), months));
            Assert.AreEqual("FV/18/10", dataFormater.FormateDateToCodeLot(new DateTime(2018, 2, 10), months));
            Assert.AreEqual("MR/22/28", dataFormater.FormateDateToCodeLot(new DateTime(2022, 3, 28), months));
        }

        [TestMethod]
        public void FormateDateToCodeLotTestExceptions()
        {
            dataFormater = serviceProvider.GetService<IDateFormater>();
            DateTime date = new DateTime();
            Assert.ThrowsException<Exception>(() => dataFormater.FormateDateToCodeLot(date, null));
            string[] months = { "JV", "FV", "MR", "AV", "MI", "JN", "JL", "AO" };
            Assert.ThrowsException<Exception>(() => dataFormater.FormateDateToCodeLot(new DateTime(2018, 1, 1), months));
        }
    }
}
