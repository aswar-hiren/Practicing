using DataLayer.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;


namespace hallocdoc.Helpers
{
    public partial class AuthManager
    {
        private readonly HellodocPrjContext _context;

       public AuthManager(HellodocPrjContext context)
        {
            _context = context;
        }
    }

    public class CustomAuthorize : Attribute, IAuthorizationFilter
    {
        private readonly string _role;
        public CustomAuthorize(string role = "")
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
  
            var request = filterContext.HttpContext.Request;
            var token = request.Cookies["jwt"];
            if (token == null || !(Temp.ValidateToken(token, "this is my custom Secret key for authentication")))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "PatientLoginPage" }));
                return;
            }
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var roleClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role);

            if (roleClaim == null || string.IsNullOrWhiteSpace(_role) || roleClaim.Value != _role)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Patient", action = "PatientLoginPage" }));
                return;
            }


        }
    }
}
