using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Pages;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers
{
    public class ArticlePagePartialController : PartialContentController<ArticlePage>
    {
        public override ActionResult Index(ArticlePage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            return PartialView("~/Views/Shared/PagePartials/ArticlePage.cshtml", currentPage);
        }
    }
}