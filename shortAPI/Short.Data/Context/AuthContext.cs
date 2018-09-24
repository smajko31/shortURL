using Microsoft.AspNet.Identity.EntityFramework;

namespace Short.Data.Context
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext() : base("AuthContext")
        {

        }
    }
}
