using LMG.FAB.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Tests.Services.tests.Mocks
{

    public class TestUserService : IUserService
    {

        public string GetCurrentDbUser()
        {
            return "TestUser";
        }

        public string GetCurrentUser()
        {
            return "TestUser";
        }

        public string TransformUser(string userLogin)
        {
            return userLogin;
        }
    }
}
