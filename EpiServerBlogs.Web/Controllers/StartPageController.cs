using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;

namespace EpiServerBlogs.Web.Controllers
{
    public class StartPageController : PageControllerBase<StartPage>
    {
        public ActionResult Index(StartPage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var model = SitePageViewModel.Create(currentPage);
            return View(model);
        }
    }
}