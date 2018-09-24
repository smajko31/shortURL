using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Short.Data.Context;
using Short.Data.Models;
using System;
using System.Threading.Tasks;

namespace Short.Data
{
    public class AuthRepository : IDisposable
    {
        private AuthContext ctx;
        private UserManager<IdentityUser> userManager;

        public AuthRepository()
        {
            this.ctx = new AuthContext();
            this.userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(this.ctx));
        }

        public async Task<IdentityResult> RegisterUser(User userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await this.userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await this.userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            this.ctx.Dispose();
            this.userManager.Dispose();
        }
    }
}
