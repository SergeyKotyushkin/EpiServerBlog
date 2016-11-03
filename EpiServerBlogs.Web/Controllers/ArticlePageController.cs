using System.Linq;
using System.Web.Mvc;
using EpiServerBlogs.Web.Models.DynamicData;
using EpiServerBlogs.Web.Models.Pages;
using EpiServerBlogs.Web.ViewModels;
using EpiServerBlogs.Web.ViewModels.Dto;
using EPiServer.Security;

namespace EpiServerBlogs.Web.Controllers
{
    public class ArticlePageController : PageControllerBase<ArticlePage>
    {
        public ActionResult Index(ArticlePage currentPage)
        {
            /* Implementation of action. You can create your own view model class that you pass to the view or
             * you can pass the page type for simpler templates */

            var model = new ArticlePageViewModel(currentPage)
            {
                Comments = Comment.GetComments(currentPage.PageLink).Select(CommentDto.FromComment).ToArray()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveComment(ArticlePage currentPage, string commentText)
        {
            if (!string.IsNullOrWhiteSpace(commentText))
            {
                //var currentPage = PageContext.Page as ArticlePage;
                var isAuthenticated = PrincipalInfo.CurrentPrincipal.Identity.IsAuthenticated;
                var username = isAuthenticated
                    ? PrincipalInfo.CurrentPrincipal.Identity.Name
                    : "Non-authentificated";

                var comment = new Comment(currentPage.PageLink.ID, username, commentText);
                comment.Save();
            }

            return RedirectToAction("Index");
        }
    }
}