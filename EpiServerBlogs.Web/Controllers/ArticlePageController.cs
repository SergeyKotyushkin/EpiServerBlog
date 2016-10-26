using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;

namespace EpiServerBlogs.Web.Controllers
{
    public class ArticlePageController : PageControllerBase<ArticlePage>
    {
        public ActionResult Index(ArticlePage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var model = SitePageViewModel.Create(currentPage);
            return View(model);
        }
    }
}