using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;


namespace BikeMates.Service.Controllers
{
    [Authorize]
    public class BaseController : ApiController
    {
        protected string userId
        { 
            get
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                return principal.Claims.Single(c => c.Type == "id").Value;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

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
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
