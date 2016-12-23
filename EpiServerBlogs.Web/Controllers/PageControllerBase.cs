using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers
{
    public class PageControllerBase<T> : PageController<T> where T : SitePageData
    {
        public ActionResult TopLinksPartial(T currentPage)
        {
            var user = User.Identity;
            var loginPage = DataFactory.Instance.GetChildren<LoginPage>(ContentReference.StartPage).FirstOrDefault();

            return PartialView("PagePartials/TopLinksPartial", new TopLinksViewModel
            {
                IsAuthenticated = user.IsAuthenticated,
                UserName = user.Name,
                ReturnUrl = Request.RawUrl,
                Language = ContentLanguage.PreferredCulture.Name,
                LoginPageLink = loginPage == null ? null : loginPage.PageLink,
                IsLoginPage = currentPage is LoginPage
            });
        }
    }
}