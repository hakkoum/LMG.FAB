using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace LMG.FAB.Util
{

    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;
        public UserService(IHttpContextAccessor context)
        {
            _context = context;

        }

        public virtual string GetCurrentDbUser()
        {
            var userLogin = _context?.HttpContext?.User?.Identity?.Name;
            var dbLogin = TransformUser(userLogin);

            return dbLogin;
        }

        public virtual string GetCurrentUser()
        {
            return _context?.HttpContext?.User?.Identity?.Name;
        }

        public virtual string TransformUser(string userLogin)
        {
            //transform user login from GRP-MARTINIERE\userId to userId@lamartiniere
            var dbLogin = userLogin?.Replace(@"GRP-MARTINIERE\", "") + "@lamartiniere";

            return dbLogin;
        }
    }
}
