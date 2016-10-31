using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.ServiceLocation;

namespace EpiServerBlogs.Web.Controllers
{
    public class ArticlePageController : PageControllerBase<ArticlePage>
    {
        public ActionResult Index(ArticlePage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            object model;
            if (!currentPage.ArticleLink.IsEmpty())
            {
                model = SitePageViewModel.Create(currentPage);
                return View(model);
            }

            var rep = ServiceLocator.Current.GetInstance<IContentRepository>();

            var writableClonePage = (ArticlePage) currentPage.CreateWritableClone();

            writableClonePage.ArticleLink = Global.GetVirtualPath(currentPage.ContentLink);
            rep.Save(writableClonePage, SaveAction.Publish, AccessLevel.NoAccess);

            model = SitePageViewModel.Create(writableClonePage);
            return View(model);
        }
    }
}