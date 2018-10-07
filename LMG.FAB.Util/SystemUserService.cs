using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Util
{
    public class SystemUserService : IUserService
    {

        public string GetCurrentDbUser()
        {
            return "System";
        }

        public string GetCurrentUser()
        {
            return "System";
        }

        public string TransformUser(string userLogin)
        {
            return userLogin;
        }
    }
}
