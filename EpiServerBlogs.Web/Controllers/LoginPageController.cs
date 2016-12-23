using System.Web;
using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;

namespace EpiServerBlogs.Web.Controllers
{
    public class LoginPageController : PageControllerBase<LoginPage>
    {
        private readonly ISiteAuthService _siteAuthService;

        public LoginPageController(ISiteAuthService siteAuthService)
        {
            _siteAuthService = siteAuthService;
        }

        public ActionResult Index(LoginPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            
            var model = new LoginPageViewModel(currentPage);
            return View(model);
        }

        [HttpPost]
        public ActionResult LogIn(LoginPage currentPage, string username, string password)
        {
            var returnUrl = Request.UrlReferrer == null
                ? "~"
                : HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReturnUrl"] ??
                  Request.UrlReferrer.PathAndQuery;

            string errorMesage;
            if (_siteAuthService.LogIn(username, password, out errorMesage)) 
                return Redirect(returnUrl);

            return View("Index", new LoginPageViewModel(currentPage)
            {
                ShowErrorMessage = true,
                ErrorMessage = errorMesage
            });
        }

        public ActionResult LogOut()
        {
            _siteAuthService.LogOut();
            return Redirect(Request.UrlReferrer == null ? "~" : Request.UrlReferrer.PathAndQuery);
        }
    }
}