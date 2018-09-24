using Microsoft.AspNet.Identity;
using Short.Data;
using Short.Data.Models;
using System.Threading.Tasks;
using System.Web.Http;

namespace Short.API.Controllers
{
    [RoutePrefix("api/Account")]
    public class AuthController : ApiController
    {
        private AuthRepository repo = null;

        public AuthController()
        {
            this.repo = new AuthRepository();
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(User userModel)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            IdentityResult result = await this.repo.RegisterUser(userModel);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null) { return errorResult; }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { this.repo.Dispose(); }
            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null) { return InternalServerError(); }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }
            return null;
        }
    }
}
