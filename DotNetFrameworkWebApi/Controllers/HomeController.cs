using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace DotNetFrameworkWebApi.Controllers
{
    [System.Web.Http.Authorize]
    public class HomeController : ApiController
    {
        private ClaimsIdentity userClaims;

        public HomeController()
        {
            var user = HttpContext.Current.User;
            userClaims = HttpContext.Current.User.Identity as ClaimsIdentity;
        }

        public string Get()
        {
            ValidateAppRole("access_as_application");

            return "Hello World!";
        }

        private void CheckAccessTokenScope(string scopeName)
        {
            // Check for scopes 

            // Make sure access_as_user scope is present
            string scopeClaimValue = userClaims.FindFirst("http://schemas.microsoft.com/identity/claims/scope")?.Value;
            if (!string.Equals(scopeClaimValue, scopeName, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    ReasonPhrase = @"Please request an access token to scope '{scopeName}'"
                });
            }
        }

        private void ValidateAppRole(string appRole)
        {
            //Check for roles
            //
            // The `role` claim tells you what permissions the client application has in the service.
            // In this case, we look for a `role` value of `access_as_application`.
            //


            //Below is the MSC docs sample that actually does not work :)) but we can replace code below with the uncommented lines ->  HttpContext.Current.User.IsInRole(appRole)

            //Claim roleClaim2 = userClaims.FindFirst("roles");
            //Claim roleClaim = userClaims.FindFirst("http://schemas.microsoft.com/identity/claims/roles");
            //if (roleClaim == null || !roleClaim.Value.Split(' ').Contains(appRole))
            //{
            //    throw new HttpResponseException(new HttpResponseMessage
            //    {
            //        StatusCode = HttpStatusCode.Unauthorized,
            //        ReasonPhrase = $"The 'roles' claim does not contain '{appRole}' or was not found"
            //    });
            //}

            var isInRole = HttpContext.Current.User.IsInRole(appRole);
            if (isInRole == false)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ReasonPhrase = $"The 'roles' claim does not contain '{appRole}' or was not found"
                });
            }
        }
    }
}
