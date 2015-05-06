using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AuthorizationServer.Models;
using Microsoft.AspNet.Identity;

namespace AuthorizationServer.Identity
{
    public class ApplicationUserStore : IUserStore<ApplicationUser>
    {
        public void Dispose()
        {

        }

        public Task CreateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(new ApplicationUser() { Id = userId, UserName = "cahsms" });
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}