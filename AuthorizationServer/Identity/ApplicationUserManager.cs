using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using AuthorizationServer.Models;
using Microsoft.AspNet.Identity;

namespace AuthorizationServer.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager()
            : base(new ApplicationUserStore())
        {

        }

        public override Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            return base.CreateIdentityAsync(user, authenticationType);
        }
    }
}