using System.Linq;
using EpiServerBlogs.Web.Models.Data;
using EpiServerBlogs.Web.Models.DynamicData;
using EpiServerBlogs.Web.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;

namespace EpiServerBlogs.Web.ViewModels
{
    public class GadgetCommentsViewModel
    {
        public GadgetCommentsViewModel()
        {
            var rep = ServiceLocator.Current.GetInstance<IContentRepository>();

            var articles = rep.GetChildren<ArticlePage>(ContentReference.StartPage).ToArray();

            ArticleCommentses = articles.Select(article => new ArticleComments
            {
                Page = article,
                Comments = Comment.GetComments(article.PageLink)
            }).ToArray();
        }

        public ArticleComments[] ArticleCommentses { get; set; }
    }
}