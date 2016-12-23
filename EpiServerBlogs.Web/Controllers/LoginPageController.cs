using System.Web;
using System.Web.Mvc;
using EpiServerBlogs.Web.Business.Helpers;
using EpiServerBlogs.Web.Business.Services.Contracts;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers
{
    public class LoginPageController : PageController<LoginPage>
    {
        private readonly ISiteAuthService _siteAuthService;

        public LoginPageController(ISiteAuthService siteAuthService)
        {
            _siteAuthService = siteAuthService;
        }

        [HttpGet]
        public ActionResult Index(LoginPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */
            
            var siteBaseModel = SiteBaseHelper.CreateViewModel(User.Identity, currentPage);
            var model = new LoginPageViewModel(siteBaseModel);
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
            if (!_siteAuthService.LogIn(username, password, out errorMesage))
            {
                var siteBaseModel = SiteBaseHelper.CreateViewModel(User.Identity, currentPage);
                var model = new LoginPageViewModel(siteBaseModel)
                {
                    ShowErrorMessage = true,
                    ErrorMessage = errorMesage
                };
                return View("Index", model);
            }

            return Redirect(returnUrl);
        }

        public ActionResult LogOut()
        {
            _siteAuthService.LogOut();
            return Redirect(Request.UrlReferrer == null ? "~" : Request.UrlReferrer.PathAndQuery);
        }
    }
}