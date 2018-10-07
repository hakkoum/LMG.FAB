using LMG.FAB.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Tests.Util.tests
{

    [TestClass]
    public class UserServiceTest
    {
        private static ServiceProvider serviceProvider;

        [ClassInitialize]
        public static void InitTests(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            serviceProvider = services.BuildServiceProvider();

        }

        [TestMethod]
        public void TransformUserTest()
        {
            var userService = serviceProvider.GetService<IUserService>();

            var user = userService.TransformUser(@"GRP-MARTINIERE\prestadsi2");
            Assert.AreEqual("prestadsi2@lamartiniere", user);

            user = userService.TransformUser("");
            Assert.AreEqual("@lamartiniere", user); //TODO: comment gerer le cas avec une chaine vide, quel sera le retour ?
        }
    }
}
