using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
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
        protected string UserId
        { 
            get
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                return principal != null && principal.Claims.Any()? principal.Claims.Single(c => c.Type == "id").Value : String.Empty;
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
