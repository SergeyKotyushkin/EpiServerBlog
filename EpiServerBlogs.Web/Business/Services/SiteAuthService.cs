using System.Security.Claims;
using System.Web;
using System.Web.Security;
using EpiServerBlogs.Web.Business.Services.Contracts;
using Microsoft.Owin.Security;

namespace EpiServerBlogs.Web.Business.Services
{
    public class SiteAuthService : ISiteAuthService
    {
        private static IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        public bool LogIn(string username, string password, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!Membership.ValidateUser(username, password))
            {
                errorMessage = "Invalid credentials";
                return false;
            }
            
            var user = Membership.GetUser(username);
            SignIn(user);

            return true;
        }

        public void LogOut()
        {
            AuthenticationManager.SignOut();
        }

        private static void SignIn(MembershipUser user)
        {
            var claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email, ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName, ClaimValueTypes.String));
            claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "OWIN Provider", ClaimValueTypes.String));
            //claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name, ClaimValueTypes.String));

            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = true
            }, claim);
        }
    }
}