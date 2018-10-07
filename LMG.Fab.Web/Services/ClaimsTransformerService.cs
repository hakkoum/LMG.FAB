using LMG.Fab.Web.Models;
using LMG.FAB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LMG.Fab.Web.Services
{
    internal class ClaimsTransformerService : IClaimsTransformation
    {
        private IUserManager _usermanager;
        public IConfiguration _config;
        private Object dbLock = new Object();

        // Can consume services from DI as needed, including scoped DbContexts
        public ClaimsTransformerService(IHttpContextAccessor httpAccessor, IUserManager usermanager, IConfiguration configuration) : base()
        {
            _usermanager = usermanager;
            _config = configuration;
        }
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal p)
        {
            string appCode = _config.GetValue<string>("appCode");
            List<string> UserRights = new List<string>();
            lock (dbLock)
            {
                 UserRights = _usermanager.GetUserRights(p?.Identity?.Name, appCode);
            }
            var claims = new List<Claim>();

            foreach (string item in UserRights)
            {
                claims.Add(new Claim(item, "true"));
            }

            p.AddIdentity(new ClaimsIdentity(claims));
            return Task.FromResult(p);
        }
    }
}
